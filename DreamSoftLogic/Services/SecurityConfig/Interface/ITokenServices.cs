using DreamSoftModel.Models.SecurityConfig;

namespace DreamSoftLogic.Services.SecurityConfig.Interface;

public interface ITokenServices
{
    Task<TokenResponse> GetAccessToken(string username, string password, string ipaddress);
    Task<TokenResponse> GetRefreshToken(string refreshToken, string ipaddress);
}