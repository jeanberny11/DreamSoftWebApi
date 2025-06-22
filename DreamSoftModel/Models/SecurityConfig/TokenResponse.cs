using DreamSoftModel.Models.Public;

namespace DreamSoftModel.Models.SecurityConfig;

public class TokenResponse
{
    public User User { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}