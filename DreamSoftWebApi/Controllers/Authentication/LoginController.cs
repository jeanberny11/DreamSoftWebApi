using DreamSoftLogic.Exceptions.Security;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DreamSoftWebApi.Controllers.authentication;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class LoginController(ILoginServices tokenServices, IOptions<JwtSetting> jwtOptions, IPasswordResetServices passwordResetServices) : ControllerBase
{
    private readonly JwtSetting _jwtSetting = jwtOptions.Value;
    private readonly IPasswordResetServices _passwordResetServices = passwordResetServices;

    [HttpPost]
    [Route("[action]")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        var result = await tokenServices.LoginAsync(request, ipaddress);
        var refreshToken = await tokenServices.CreateRefreshToken(result.User.UserId, ipaddress);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSetting.AccessTokenExpirationMinutes)),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/"
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        return result;
    }


    [HttpPost]
    [Route("[action]")]
    public async Task<LoginResponse> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"] ?? throw new UnAuthorizedUserException("No refresh token found in cookies.");
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        var result = await tokenServices.GetRefreshToken(refreshToken, ipaddress);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSetting.AccessTokenExpirationMinutes)),
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/"
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        return result;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Logout()
    {
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        Response.Cookies.Delete("refreshToken");
        var refreshToken = Request.Cookies["refreshToken"];
        if(refreshToken != null)
        {
            await tokenServices.LogoutAsync(refreshToken, ipaddress);
        }
        return Ok(new { message = "Logged out successfully." });
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<AuthOperationResponse> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        // Extract frontend URL from Origin header
        var frontendUrl = Request.Headers["Origin"].ToString();

        if (string.IsNullOrEmpty(frontendUrl))
        {
            // Fallback to Referer if Origin is not present
            frontendUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(frontendUrl))
            {
                // Extract base URL from Referer (remove path)
                var uri = new Uri(frontendUrl);
                frontendUrl = $"{uri.Scheme}://{uri.Authority}";
            }
            else
            {
                // Last fallback - use default
                frontendUrl = "http://localhost:5173";
            }
        }

        var result = await _passwordResetServices.ForgotPasswordAsync(request, frontendUrl);
        return result;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<AuthOperationResponse> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _passwordResetServices.ResetPasswordAsync(request);
        return result;
    }
}