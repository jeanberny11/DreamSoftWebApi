using DreamSoftModel.Models.Public;
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
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international format.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
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
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
                .Must(HasUpperCase).WithMessage("Password must contain at least one uppercase letter.")
                .Must(HasLowerCase).WithMessage("Password must contain at least one lowercase letter.")
                .Must(HasDigit).WithMessage("Password must contain at least one number.");
            RuleFor(x=>x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Username can only contain letters and numbers.");
        }

        private bool HasUpperCase(string password) => password.Any(char.IsUpper);
        private bool HasLowerCase(string password) => password.Any(char.IsLower);
        private bool HasDigit(string password) => password.Any(char.IsDigit);
        //private bool HasSpecialChar(string password) =>
        //    Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]");
    }
}
