using DreamSoftLogic.Services.SecurityConfig.Impl;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftLogic.Services.Inventory.Impl;
using DreamSoftLogic.Services.Inventory.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftLogic.Services.Authentication.Impl;
using DreamSoftLogic.Services.Menu.Interfaces;
using DreamSoftLogic.Services.Menu.Impl;
using DreamSoftLogic.Services.Generics.Impl;
using DreamSoftData.Entities.Authentication;
using DreamSoftLogic.Services.Email;

namespace DreamSoftLogic.Config;

public static class DreamSoftLogicServicesBuilder
{
    public static IServiceCollection AddDreamSoftLogicServices(this IServiceCollection services)
    {
        return services.AddAllServices();
    }

    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        return services.AddScoped<IGenderServices, GenderServices>()
            .AddScoped<IIdTypeServices, IdTypeServices>()
            .AddScoped<ILoginServices, LoginServices>()
            .AddScoped<IAccountServices, AccountServices>()
            .AddScoped<IAccountTypeServices, AccountTypeServices>()
            .AddScoped<IUserServices, UserServices>()
            .AddScoped<IModuleServices, ModuleServices>()
            .AddScoped<IRoleServices, RoleServices>()
            .AddScoped<IMenuGroupServices, MenuGroupServices>()
            .AddScoped<IMenuOptionServices, MenuOptionServices>()
            .AddScoped<IRoleOptionsServices, RoleOptionServices>()
            .AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>()
            // Inventory services
            .AddScoped<IBrandServices, BrandServices>()
            .AddScoped<IModelServices, ModelServices>()
            .AddScoped<IProductServices, ProductServices>()
            .AddScoped<ICategoryServices, CategoryServices>()
            .AddScoped<IUnitServices, UnitServices>()
            .AddScoped<ILocationServices, LocationServices>()
            .AddScoped<ISupplierServices, SupplierServices>()
            .AddScoped<IWarehouseServices, WarehouseServices>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<ICountryServices, CountryServices>()
            .AddScoped<IProvinceServices, ProvinceServices>()
            .AddScoped<IMunicipalityServices, MunicipalityServices>()
            .AddScoped<IGenderServices, GenderServices>()
            .AddScoped<IIdTypeServices, IdTypeServices>();
    }
}