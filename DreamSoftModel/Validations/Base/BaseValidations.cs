using FluentValidation;

namespace DreamSoftModel.Validations.Base
{
    public static class BaseValidations
    {
        public static IRuleBuilderOptions<T, string> DescriptionRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
        }

        public static IRuleBuilderOptions<T, string> NameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        }

        public static IRuleBuilderOptions<T, string> PhoneRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international format.");
        }

        public static IRuleBuilderOptions<T, string> EmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
        }

        public static IRuleBuilderOptions<T, string> PasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
                .Must(HasUpperCase).WithMessage("Password must contain at least one uppercase letter.")
                .Must(HasLowerCase).WithMessage("Password must contain at least one lowercase letter.")
                .Must(HasDigit).WithMessage("Password must contain at least one number.");
        }

        public static IRuleBuilderOptions<T, string> UserNameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Username can only contain letters and numbers.");
        }

        public static IRuleBuilderOptions<T, int> AccountIdRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("El registro ebe estar atado a una cuenta de usuario")
                .GreaterThan(0).WithMessage("El registro ebe estar atado a una cuenta de usuario");
        }

        public static IRuleBuilderOptions<T, string> IdNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Un numero de identificacion valida es requerido.")
                .NotEmpty().WithMessage("Un numero de identificacion valida es requerido.");
        }

        public static IRuleBuilderOptions<T, int> IdTypeRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Debe especificar el tipo de documento de identificacion.")
                .GreaterThan(0).WithMessage("Debe especificar un tipo de documento de identificacion valido.");
        }

        public static IRuleBuilderOptions<T, string> AddressRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("La direccion es requerida.")
                .NotEmpty().WithMessage("La direccion es requerida.");
        }

        public static IRuleBuilderOptions<T, int> IntRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Este valor no debe ser nulo.")
                .GreaterThan(0).WithMessage("Este valor debe ser mayor que 0.");
        }

        public static IRuleBuilderOptions<T, decimal> DecimalRule<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Este valor no debe ser nulo.")
                .GreaterThan(0).WithMessage("Este valor debe ser mayor que 0.");
        }

        public static IRuleBuilderOptions<T, int> CountryRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Debe especificar el pais.")
                .GreaterThan(0).WithMessage("Debe especificar un pais valido.");
        }

        public static IRuleBuilderOptions<T, int> ProvinceRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Debe especificar la provincia.")
                .GreaterThan(0).WithMessage("Debe especificar una provincia valida.");
        }

        public static IRuleBuilderOptions<T, int> MunicipalityRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Debe especificar el municipio.")
                .GreaterThan(0).WithMessage("Debe especificar un municipio valido.");
        }

        private static bool HasUpperCase(string password) => password.Any(char.IsUpper);
        private static bool HasLowerCase(string password) => password.Any(char.IsLower);
        private static bool HasDigit(string password) => password.Any(char.IsDigit);
        //private bool HasSpecialChar(string password) =>
        //   Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]");
    }
}
