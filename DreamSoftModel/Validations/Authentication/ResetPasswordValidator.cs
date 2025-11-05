using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Validations.Base;
using FluentValidation;

namespace DreamSoftModel.Validations.Authentication;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token es requerido.");

        RuleFor(x => x.NewPassword).PasswordRule();

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Debe confirmar la contraseña.")
            .Equal(x => x.NewPassword).WithMessage("Las contraseñas no coinciden.");
    }
}
