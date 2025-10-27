using DreamSoftLogic.Exceptions.Security;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DreamSoftWebApi.Controllers.authentication;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class LoginController(ILoginServices tokenServices,IOptions<JwtSetting> jwtOptions) : ControllerBase
{
    private readonly JwtSetting _jwtSetting = jwtOptions.Value;

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
        var refreshToken = Request.Cookies["refreshToken"] ?? throw new UnAuthorizedUserException("No refresh token found in cookies.");
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        await tokenServices.LogoutAsync(refreshToken, ipaddress);
        Response.Cookies.Delete("refreshToken");
        return Ok(new { message = "Logged out successfully." });
    }
}