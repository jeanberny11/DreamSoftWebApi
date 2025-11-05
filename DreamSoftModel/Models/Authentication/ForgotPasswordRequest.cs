namespace DreamSoftModel.Models.Authentication;

public class ForgotPasswordRequest
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
}
