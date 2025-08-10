using DreamSoftModel.Models.Inventory;
using DreamSoftModel.Validations.Base;
using FluentValidation;

namespace DreamSoftModel.Validations.Inventory
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator() {
            RuleFor(x => x.AccountId).AccountIdRule();
            RuleFor(x=>x.Name).NameRule();
            RuleFor(x => x.IdNumber).IdNumberRule();
            RuleFor(x=>x.IdTypeId).IdTypeRule();
            RuleFor(x=>x.PhoneNumber).PhoneRule();
            RuleFor(x => x.CellNumber).PhoneRule();
            RuleFor(x => x.Email).EmailRule();
            RuleFor(x => x.Address).AddressRule();
            RuleFor(x => x.CountryId).CountryRule();
            RuleFor(x => x.ProvinceId).ProvinceRule();
            RuleFor(x => x.MunicipalityId).MunicipalityRule();
            RuleFor(x => x.CreditLimit).DecimalRule();
            RuleFor(x => x.CreditDays).IntRule();
        }
    }
}
