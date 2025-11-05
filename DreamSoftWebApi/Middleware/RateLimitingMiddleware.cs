using System.Collections.Concurrent;
using System.Net;

namespace DreamSoftWebApi.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    // Store failed attempts: Key = IP Address, Value = (Count, LastAttemptTime)
    private static readonly ConcurrentDictionary<string, (int Count, DateTime LastAttempt)> FailedAttempts = new();

    // Configuration
    private const int MaxFailedAttempts = 5;
    private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan ResetWindow = TimeSpan.FromMinutes(5);

    public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

        // Only apply rate limiting to the actual login endpoint, not other endpoints in LoginController
        if (path.EndsWith("/login") && context.Request.Method == "POST")
        {
            // Check if IP is currently locked out
            if (IsLockedOut(ipAddress))
            {
                _logger.LogWarning("Rate limit exceeded for IP: {IpAddress}. Access temporarily blocked.", ipAddress);
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Too many failed login attempts. Please try again later.\"}");
                return;
            }

            // Store the original response body stream
            var originalBodyStream = context.Response.Body;

            try
            {
                // Create a new memory stream to capture the response
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                // Call the next middleware
                await _next(context);

                // Check the response status code
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    RecordFailedAttempt(ipAddress);
                    _logger.LogWarning("Failed login attempt recorded for IP: {IpAddress}. Total attempts: {Attempts}",
                        ipAddress, GetFailedAttemptCount(ipAddress));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    // Clear failed attempts on successful login
                    ClearFailedAttempts(ipAddress);
                    _logger.LogInformation("Successful login, cleared failed attempts for IP: {IpAddress}", ipAddress);
                }

                // Copy the response back to the original stream
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private static bool IsLockedOut(string ipAddress)
    {
        if (!FailedAttempts.TryGetValue(ipAddress, out var attempt))
            return false;

        if (attempt.Count >= MaxFailedAttempts)
        {
            var timeSinceLastAttempt = DateTime.UtcNow - attempt.LastAttempt;
            if (timeSinceLastAttempt < LockoutDuration)
                return true;

            // Lockout period has expired, clear the attempts
            FailedAttempts.TryRemove(ipAddress, out _);
        }

        return false;
    }

    private static void RecordFailedAttempt(string ipAddress)
    {
        FailedAttempts.AddOrUpdate(
            ipAddress,
            (1, DateTime.UtcNow),
            (key, existing) =>
            {
                var timeSinceLastAttempt = DateTime.UtcNow - existing.LastAttempt;

                // Reset count if the reset window has passed
                if (timeSinceLastAttempt > ResetWindow)
                    return (1, DateTime.UtcNow);

                return (existing.Count + 1, DateTime.UtcNow);
            }
        );
    }

    private static void ClearFailedAttempts(string ipAddress)
    {
        FailedAttempts.TryRemove(ipAddress, out _);
    }

    private static int GetFailedAttemptCount(string ipAddress)
    {
        return FailedAttempts.TryGetValue(ipAddress, out var attempt) ? attempt.Count : 0;
    }
}
