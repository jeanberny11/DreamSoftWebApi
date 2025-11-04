using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace DreamSoftLogic.Services.Email
{
    /// <summary>
    /// Manages verification codes with in-memory storage and expiration
    /// </summary>
    public interface IVerificationCodeManager
    {
        string GenerateCode(string email);
        bool ValidateCode(string email, string code);
        void RemoveCode(string email);
    }

    public class VerificationCodeManager : IVerificationCodeManager
    {
        // In-memory storage for verification codes
        // Key: email, Value: (code, expiryTime)
        private readonly ConcurrentDictionary<string, (string Code, DateTime ExpiryTime)> _verificationCodes;
        private readonly ILogger<VerificationCodeManager> _logger;
        private readonly TimeSpan _codeExpiryDuration = TimeSpan.FromMinutes(5);

        public VerificationCodeManager(ILogger<VerificationCodeManager> logger)
        {
            _verificationCodes = new ConcurrentDictionary<string, (string, DateTime)>();
            _logger = logger;

            // Start cleanup task to remove expired codes
            StartCleanupTask();
        }

        /// <summary>
        /// Generates a new 6-digit verification code for the email
        /// Overwrites any existing code for this email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>6-digit verification code</returns>
        public string GenerateCode(string email)
        {
            // Generate random 6-digit code
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();
            var expiryTime = DateTime.UtcNow.Add(_codeExpiryDuration);

            // Store code (overwrites existing if any)
            _verificationCodes[email.ToLowerInvariant()] = (code, expiryTime);

            _logger.LogInformation("Verification code generated for {Email}, expires at {ExpiryTime}", 
                email, expiryTime);

            return code;
        }

        /// <summary>
        /// Validates the verification code for the email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="code">6-digit verification code</param>
        /// <returns>True if code is valid and not expired, false otherwise</returns>
        public bool ValidateCode(string email, string code)
        {
            var emailKey = email.ToLowerInvariant();

            if (!_verificationCodes.TryGetValue(emailKey, out var storedData))
            {
                _logger.LogWarning("No verification code found for {Email}", email);
                return false;
            }

            // Check if code has expired
            if (DateTime.UtcNow > storedData.ExpiryTime)
            {
                _logger.LogWarning("Verification code expired for {Email}", email);
                _verificationCodes.TryRemove(emailKey, out _);
                return false;
            }

            // Check if code matches
            if (storedData.Code != code)
            {
                _logger.LogWarning("Invalid verification code provided for {Email}", email);
                return false;
            }

            _logger.LogInformation("Verification code validated successfully for {Email}", email);
            return true;
        }

        /// <summary>
        /// Removes the verification code for the email
        /// Should be called after successful verification
        /// </summary>
        /// <param name="email">Email address</param>
        public void RemoveCode(string email)
        {
            var emailKey = email.ToLowerInvariant();
            _verificationCodes.TryRemove(emailKey, out _);
            _logger.LogInformation("Verification code removed for {Email}", email);
        }

        /// <summary>
        /// Background task to periodically clean up expired codes
        /// Runs every minute
        /// </summary>
        private void StartCleanupTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1));

                        var now = DateTime.UtcNow;
                        var expiredKeys = _verificationCodes
                            .Where(kvp => kvp.Value.ExpiryTime < now)
                            .Select(kvp => kvp.Key)
                            .ToList();

                        foreach (var key in expiredKeys)
                        {
                            _verificationCodes.TryRemove(key, out _);
                        }

                        if (expiredKeys.Count > 0)
                        {
                            _logger.LogInformation("Cleaned up {Count} expired verification codes", 
                                expiredKeys.Count);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error in verification code cleanup task");
                    }
                }
            });
        }
    }
}