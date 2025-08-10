using DreamSoftData.Entities.Public;
using DreamSoftLogic.Services.Public.Impl;
using DreamSoftLogic.Services.Public.Interface;
using DreamSoftLogic.Services.SecurityConfig.Impl;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftLogic.Services.Inventory.Impl;
using DreamSoftLogic.Services.Inventory.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
            .AddScoped<ITokenServices, TokenServices>()
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
            .AddScoped<IWarehouseServices, WarehouseServices>();
    }
}