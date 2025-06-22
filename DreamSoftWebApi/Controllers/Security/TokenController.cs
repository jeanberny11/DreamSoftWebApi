using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Security;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class TokenController(ITokenServices tokenServices) : ControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public Task<TokenResponse> GenerateAccessToken([FromBody] TokenBody tokenBody)
    {
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        return tokenServices.GetAccessToken(tokenBody.UserName, tokenBody.Password, ipaddress);
    }


    [HttpPost]
    [Route("[action]")]
    public Task<TokenResponse> GenerateRefreshToken(string refreshToken)
    {
        var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        return tokenServices.GetRefreshToken(refreshToken, ipaddress);
    }
}