using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Validations.Base;
using FluentValidation;

namespace DreamSoftModel.Validations.Authentication;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).EmailRule();
        RuleFor(x => x.Username).UserNameRule();
    }
}
