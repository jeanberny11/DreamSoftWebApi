using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Validations.Base
{
    public class DescriptionRequiredAttribute: ValidationAttribute
    {

        private readonly int _maxLength;

        public DescriptionRequiredAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("La descripcion no debe estar en blanco");
            }
            if (value.ToString()!.Length > _maxLength)
            {
                return new ValidationResult($"La descripcion no debe ser mayor a {_maxLength} caracteres.");
            }
            return ValidationResult.Success;
        }
    }
}
