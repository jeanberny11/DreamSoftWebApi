using Resend;
using Microsoft.Extensions.Options;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.Extensions.Logging;

namespace DreamSoftLogic.Services.Email;

/// <summary>
/// Servicio para enviar correos electrónicos usando la API de Resend
/// </summary>
public interface IEmailService
{
    Task<bool> SendVerificationCodeAsync(string toEmail, string verificationCode);
    Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string username, string frontendUrl);
}

public class EmailService(
    IResend resend,
    IOptions<EmailSettings> emailSettings,
    ILogger<EmailService> logger) : IEmailService
{
    private readonly IResend _resend = resend;
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly ILogger<EmailService> _logger = logger;

    /// <summary>
    /// Envía un correo electrónico con código de verificación al usuario
    /// </summary>
    /// <param name="toEmail">Dirección de correo electrónico del destinatario</param>
    /// <param name="verificationCode">Código de verificación de 6 dígitos</param>
    /// <returns>Verdadero si el correo se envió exitosamente, falso en caso contrario</returns>
    public async Task<bool> SendVerificationCodeAsync(string toEmail, string verificationCode)
    {
        try
        {
            var message = new EmailMessage
            {
                From = _emailSettings.FromEmail
            };
            message.To.Add(toEmail);
            message.Subject = "Verifica Tu Correo Electrónico - DreamSoft";
            message.HtmlBody = GetVerificationEmailHtml(verificationCode);

            var response = await _resend.EmailSendAsync(message);

            if (response.Success)
            {
                _logger.LogInformation("Correo de verificación enviado exitosamente a {Email}", toEmail);
                return true;
            }
            else
            {
                _logger.LogError("Error al enviar correo de verificación a {Email}. Estado: {Status}",
                    toEmail, response.Exception?.Message);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar correo de verificación a {Email}", toEmail);
            return false;
        }
    }

    public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string username, string frontendUrl)
    {
        try
        {
            var resetLink = $"{frontendUrl}/reset-password/{resetToken}";

            var message = new EmailMessage
            {
                From = _emailSettings.FromEmail
            };
            message.To.Add(toEmail);
            message.Subject = "Restablecimiento de Contraseña - DreamSoft";
            message.HtmlBody = GetPasswordResetEmailHtml(resetToken, resetLink, username, toEmail);

            var response = await _resend.EmailSendAsync(message);

            if (response.Success)
            {
                _logger.LogInformation("Correo de restablecimiento enviado a {Email} para usuario {Username}",
                    toEmail, username);
                return true;
            }
            else
            {
                _logger.LogError("Error al enviar correo de restablecimiento a {Email}. Estado: {Status}",
                    toEmail, response.Exception?.Message);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar correo de restablecimiento a {Email}", toEmail);
            return false;
        }
    }

    /// <summary>
    /// Genera el cuerpo HTML del correo electrónico para el código de verificación
    /// </summary>
    private string GetVerificationEmailHtml(string verificationCode)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .container {{
            background-color: #f9f9f9;
            border-radius: 10px;
            padding: 30px;
            border: 1px solid #e0e0e0;
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .logo {{
            font-size: 24px;
            font-weight: bold;
            color: #14b8a6;
        }}
        .code-container {{
            background-color: #ffffff;
            border: 2px solid #14b8a6;
            border-radius: 8px;
            padding: 20px;
            text-align: center;
            margin: 30px 0;
        }}
        .code {{
            font-size: 32px;
            font-weight: bold;
            color: #14b8a6;
            letter-spacing: 8px;
        }}
        .footer {{
            text-align: center;
            margin-top: 30px;
            font-size: 12px;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>DreamSoft</div>
            <h2>Verificación de Correo Electrónico</h2>
        </div>

        <p>Hola,</p>

        <p>¡Gracias por registrarte en DreamSoft! Para completar tu registro, por favor usa el código de verificación a continuación:</p>

        <div class='code-container'>
            <div class='code'>{verificationCode}</div>
        </div>

        <p><strong>Este código expirará en 5 minutos.</strong></p>

        <p>Si no solicitaste este código, por favor ignora este correo electrónico.</p>

        <div class='footer'>
            <p>© 2025 DreamSoft. Todos los derechos reservados.</p>
            <p>Este es un correo electrónico automatizado, por favor no respondas.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string GetPasswordResetEmailHtml(string resetToken, string resetLink, string username, string email)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .container {{
            background-color: #f9f9f9;
            border-radius: 10px;
            padding: 30px;
            border: 1px solid #e0e0e0;
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .logo {{
            font-size: 24px;
            font-weight: bold;
            color: #14b8a6;
        }}
        .content {{
            background-color: #ffffff;
            border-radius: 8px;
            padding: 20px;
            margin: 20px 0;
        }}
        .user-info {{
            background-color: #f0f9ff;
            border-left: 4px solid #14b8a6;
            padding: 15px;
            margin: 20px 0;
        }}
        .button {{
            display: inline-block;
            background-color: #14b8a6;
            color: #ffffff;
            padding: 12px 30px;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
            margin: 20px 0;
        }}
        .button:hover {{
            background-color: #0f9688;
        }}
        .warning {{
            background-color: #fef3c7;
            border-left: 4px solid #f59e0b;
            padding: 15px;
            margin: 20px 0;
        }}
        .footer {{
            text-align: center;
            margin-top: 30px;
            font-size: 12px;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>DreamSoft</div>
            <h2>Restablecimiento de Contraseña</h2>
        </div>

        <div class='content'>
            <p>Hola,</p>

            <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</p>

            <div class='user-info'>
                <strong>Información de la cuenta:</strong><br>
                Usuario: <strong>{username}</strong><br>
                Correo: <strong>{email}</strong>
            </div>

            <p>Haz clic en el siguiente botón para restablecer tu contraseña:</p>

            <center>
                <a href='{resetLink}' class='button'>Restablecer Contraseña</a>
            </center>

            <p>O copia y pega este enlace en tu navegador:</p>
            <p style='word-break: break-all; color: #14b8a6;'>{resetLink}</p>

            <div class='warning'>
                <strong>⚠️ Importante:</strong><br>
                • Este enlace expirará en <strong>15 minutos</strong><br>
                • Solo puede ser usado una vez<br>
                • Si no solicitaste este cambio, ignora este correo
            </div>

            <p>Por seguridad, tu contraseña no será cambiada hasta que accedas al enlace de arriba y crees una nueva.</p>
        </div>

        <div class='footer'>
            <p>© 2025 DreamSoft. Todos los derechos reservados.</p>
            <p>Este es un correo electrónico automatizado, por favor no respondas.</p>
        </div>
    </div>
</body>
</html>";
    }
}
