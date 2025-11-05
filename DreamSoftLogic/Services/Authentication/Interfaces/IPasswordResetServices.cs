using DreamSoftModel.Models.Authentication;

namespace DreamSoftLogic.Services.Authentication.Interfaces;

public interface IPasswordResetServices
{
    Task<AuthOperationResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string frontendUrl);
    Task<AuthOperationResponse> ResetPasswordAsync(ResetPasswordRequest request);
}
