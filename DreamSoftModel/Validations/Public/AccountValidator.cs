using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Validations.Base;
using FluentValidation;

namespace DreamSoftModel.Validations.Public
{
    public class AccountValidator : AbstractValidator<AccountCreate>
    {
        public AccountValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");
            RuleFor(x => x.Phone).PhoneRule();
            RuleFor(x => x.Email).EmailRule();
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Country)
                .NotNull().WithMessage("Country is required.");
            RuleFor(x => x.Province)
                .NotNull().WithMessage("Province is required.");
            RuleFor(x => x.Municipality)
                .NotNull().WithMessage("Municipality is required.");
            RuleFor(x => x.AccountType)
                .NotNull().WithMessage("Account type is required.");
            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Gender is required.");
            RuleFor(x => x.IdNumber)
                .NotEmpty().WithMessage("Id number is required.")
                .MaximumLength(50).WithMessage("Id number must not exceed 50 characters.");
            RuleFor(x => x.IdType)
                .NotNull().WithMessage("Id type is required.");
            RuleFor(x => x.Password).PasswordRule();
            RuleFor(x=>x.UserName).UserNameRule();
        }

       
    }
}
