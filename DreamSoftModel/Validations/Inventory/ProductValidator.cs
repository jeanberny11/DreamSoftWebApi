using DreamSoftModel.Models.Inventory;
using DreamSoftModel.Validations.Base;
using FluentValidation;

namespace DreamSoftModel.Validations.Inventory
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.AccountId).AccountIdRule();
            RuleFor(x => x.Reference)
                .NotNull().WithMessage("El producto debe tener un id de referencia.")
                .NotEmpty().WithMessage("El producto debe tener un id de referencia.");            
            RuleFor(x => x.Name).DescriptionRule();            
            RuleFor(x => x.Brand).NotNull().WithMessage("Debe especificar la marca.");
            RuleFor(x => x.Model).NotNull().WithMessage("Debe especificar el modelo.");
            RuleFor(x => x.Category).NotNull().WithMessage("Debe especificar la categoria.");
            RuleFor(x => x.Unit).NotNull().WithMessage("Debe especificar la unidad de medida.");
            RuleFor(x => x.Location).NotNull().WithMessage("Debe especificar la ubicacion e producto.");
            RuleFor(x => x.LastCost).DecimalRule();
            RuleFor(x => x.TaxPercent).InclusiveBetween(0, 100).WithMessage("Tax Percent must be between 0 and 100.");
            RuleFor(x => x.MaxDiscount).DecimalRule();
            RuleFor(x => x.ReorderPoint).DecimalRule();
        }
    }
}
