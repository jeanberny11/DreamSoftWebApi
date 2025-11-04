using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Models.Email
{
    /// <summary>
    /// Request to send verification code to email
    /// </summary>
    public class SendVerificationCodeRequest
    {
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response after sending verification code
    /// </summary>
    public class SendVerificationCodeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request to verify email code
    /// </summary>
    public class VerifyEmailCodeRequest
    {
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de verificación es requerido")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El código de verificación debe tener exactamente 6 dígitos")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "El código de verificación debe contener solo números")]
        public string Code { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response after verifying email code
    /// </summary>
    public class VerifyEmailCodeResponse
    {
        public bool Success { get; set; }
        public bool Verified { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}