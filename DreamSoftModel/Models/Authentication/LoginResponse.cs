using DreamSoftModel.Models.Authentication;

namespace DreamSoftModel.Models.Authentication;

public class LoginResponse
{
    public User User { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
}