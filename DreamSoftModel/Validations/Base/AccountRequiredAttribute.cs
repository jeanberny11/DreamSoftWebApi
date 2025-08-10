using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Validations.Base
{
    public class AccountRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null )
            {
                return new ValidationResult("El registro debe estar atado a una cuenta de usuario");
            }
            if (value is int intvalue)
            {
                if(intvalue <= 0)
                {
                    return new ValidationResult("El registro debe estar atado a una cuenta de usuario valida");
                }
            }
            else
            {
                return new ValidationResult("Tipo de dato invalido");
            }
            return ValidationResult.Success;
        }
    }
}
