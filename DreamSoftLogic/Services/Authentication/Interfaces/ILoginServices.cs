using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.SecurityConfig;

namespace DreamSoftLogic.Services.SecurityConfig.Interface;

public interface ILoginServices
{
    Task<LoginResponse> LoginAsync(LoginRequest request, string ipaddress);
    Task<string> CreateRefreshToken(int userid, string ipaddress);
    Task<LoginResponse> GetRefreshToken(string refreshToken, string ipaddress);
    Task LogoutAsync(string refreshToken, string ipaddress);
}