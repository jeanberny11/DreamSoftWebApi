using System.Security.Cryptography;
using System.Text;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftLogic.Services.Email;
using DreamSoftModel.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DreamSoftLogic.Services.Authentication.Impl;

public class PasswordResetServices(
    IPasswordResetTokenRepository passwordResetTokenRepository,
    IAccountRepository accountRepository,
    IUsersRepository usersRepository,
    IEmailService emailService,
    IPasswordHasher<Users> passwordHasher,
    ILogger<PasswordResetServices> logger) : IPasswordResetServices
{
    private const int MaxRequestsPerHour = 3;
    private const int TokenExpirationMinutes = 15;

    public async Task<AuthOperationResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string frontendUrl)
    {
        try
        {
            // Step 1: Validate account exists by email
            var account = await accountRepository.GetAccountByEmail(request.Email);
            if (account == null)
            {
                // Return generic success message to prevent email enumeration
                logger.LogWarning("Forgot password attempt for non-existent email: {Email}", request.Email);
                return new AuthOperationResponse
                {
                    Success = true,
                    Message = "Si la cuenta existe, recibirás un correo con instrucciones para restablecer tu contraseña."
                };
            }

            // Step 2: Validate user exists by username and account
            var user = await usersRepository.GetAuthUser(account.AccountId, request.Username);
            if (user == null)
            {
                // Return generic success message to prevent username enumeration
                logger.LogWarning("Forgot password attempt for non-existent username: {Username} on account: {Email}",
                    request.Username, request.Email);
                return new AuthOperationResponse
                {
                    Success = true,
                    Message = "Si la cuenta existe, recibirás un correo con instrucciones para restablecer tu contraseña."
                };
            }

            // Step 3: Check rate limiting (max 3 requests per hour per email+username)
            var oneHourAgo = DateTime.UtcNow.AddHours(-1);
            var recentTokenCount = await passwordResetTokenRepository.CountRecentTokensAsync(
                request.Email, request.Username, oneHourAgo);

            if (recentTokenCount >= MaxRequestsPerHour)
            {
                logger.LogWarning("Rate limit exceeded for email: {Email}, username: {Username}",
                    request.Email, request.Username);
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "Has excedido el límite de solicitudes. Por favor, intenta de nuevo más tarde."
                };
            }

            // Step 4: Revoke any existing active tokens for this user
            await passwordResetTokenRepository.RevokeUserTokensAsync(user.UserId);

            // Step 5: Generate secure token
            var token = GenerateSecureToken();
            var tokenHash = HashToken(token);

            // Step 6: Create password reset token entity
            var passwordResetToken = new PasswordResetTokens
            {
                TokenHash = tokenHash,
                UserId = user.UserId,
                AccountId = account.AccountId,
                Email = request.Email,
                Username = request.Username,
                ExpiresAt = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await passwordResetTokenRepository.CreateAsync(passwordResetToken);

            // Step 7: Send password reset email
            var emailSent = await emailService.SendPasswordResetEmailAsync(
                account.Email,
                token,
                user.UserName,
                frontendUrl);

            if (!emailSent)
            {
                logger.LogError("Failed to send password reset email to {Email}", account.Email);
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "Error al enviar el correo de restablecimiento. Por favor, intenta de nuevo."
                };
            }

            logger.LogInformation("Password reset email sent successfully to {Email} for user {Username}",
                account.Email, user.UserName);

            return new AuthOperationResponse
            {
                Success = true,
                Message = "Se ha enviado un correo con instrucciones para restablecer tu contraseña."
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ForgotPasswordAsync for email: {Email}, username: {Username}",
                request.Email, request.Username);
            return new AuthOperationResponse
            {
                Success = false,
                Message = "Ha ocurrido un error. Por favor, intenta de nuevo más tarde."
            };
        }
    }

    public async Task<AuthOperationResponse> ResetPasswordAsync(ResetPasswordRequest request)
    {
        try
        {
            // Step 1: Hash the provided token
            var tokenHash = HashToken(request.Token);

            // Step 2: Find token in database
            var passwordResetToken = await passwordResetTokenRepository.GetByTokenHashAsync(tokenHash);
            if (passwordResetToken == null)
            {
                logger.LogWarning("Invalid password reset token attempted");
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "El enlace de restablecimiento es inválido."
                };
            }

            // Step 3: Validate token not expired
            if (passwordResetToken.ExpiresAt < DateTime.UtcNow)
            {
                logger.LogWarning("Expired password reset token attempted for user: {UserId}",
                    passwordResetToken.UserId);
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "El enlace de restablecimiento ha expirado. Por favor, solicita uno nuevo."
                };
            }

            // Step 4: Validate token not already used
            if (passwordResetToken.IsUsed)
            {
                logger.LogWarning("Already used password reset token attempted for user: {UserId}",
                    passwordResetToken.UserId);
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "Este enlace ya ha sido utilizado. Por favor, solicita uno nuevo."
                };
            }

            // Step 5: Get user
            var user = await usersRepository.GetByIdAsync(passwordResetToken.UserId);
            if (user == null)
            {
                logger.LogError("User not found for password reset token: {UserId}", passwordResetToken.UserId);
                return new AuthOperationResponse
                {
                    Success = false,
                    Message = "Error al restablecer la contraseña. Usuario no encontrado."
                };
            }

            // Step 6: Hash new password
            user.Password = passwordHasher.HashPassword(user, request.NewPassword);

            // Step 7: Update user password
            await usersRepository.UpdateAsync(user);

            // Step 8: Mark token as used
            passwordResetToken.IsUsed = true;
            await passwordResetTokenRepository.UpdateAsync(passwordResetToken);

            logger.LogInformation("Password reset successful for user: {UserId}, username: {Username}",
                user.UserId, user.UserName);

            return new AuthOperationResponse
            {
                Success = true,
                Message = "Tu contraseña ha sido restablecida exitosamente. Ahora puedes iniciar sesión con tu nueva contraseña."
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ResetPasswordAsync");
            return new AuthOperationResponse
            {
                Success = false,
                Message = "Ha ocurrido un error al restablecer la contraseña. Por favor, intenta de nuevo."
            };
        }
    }

    private static string GenerateSecureToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }

    private static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = sha256.ComputeHash(tokenBytes);
        return Convert.ToBase64String(hashBytes);
    }
}
