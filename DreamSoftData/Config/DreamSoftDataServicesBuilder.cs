using DreamSoftData.Repositories.Authentication.Impl;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftData.Repositories.Generics.Impl;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftData.Repositories.Inventory.Impl;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftData.Repositories.Menu.Impl;
using DreamSoftData.Repositories.Menu.Interfaces;
using DreamSoftData.Repositories.Sales.Impl;
using DreamSoftData.Repositories.Sales.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace DreamSoftData.Config;

public static class DreamSoftDataServicesBuilder
{
    public static IServiceCollection AddDreamSoftData(this IServiceCollection services)
    {
        return services.AddRepositoryServices();
    }

    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services.AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ICountriesRepository, CountriesRepository>()
            .AddScoped<IGendersRepository, GendersRepository>()
            .AddScoped<IMenuGroupsRepository, MenuGroupsRepository>()
            .AddScoped<IMenuOptionsRepository, MenuOptionsRepository>()
            .AddScoped<IModulesRepository, ModulesRepository>()
            .AddScoped<IMunicipalitiesRepository, MunicipalitiesRepository>()
            .AddScoped<IProvincesRepository, ProvincesRepository>()
            .AddScoped<IRoleOptionsRepository, RoleOptionsRepository>()
            .AddScoped<IRolesRepository, RolesRepository>()
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddScoped<IIdtypesRepository, IdTypesRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IDefaultValSetupsRepository,DefaultValSetupRepository>()
            .AddScoped<IAccountTypesRepository,AccountTypesRepository>()
            .AddScoped<IMaritalStatusRepository,MaritalStatusRepository>()
            .AddScoped<ITaxTypeRepository,TaxTypeRepository>()
            .AddScoped<IPricesRepository,PricesRepository>()
            .AddScoped<ICustomersRepository,CustomersRepository>()
            // Inventory repositories
            .AddScoped<IBrandsRepository, BrandsRepository>()
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<ILocationsRepository, LocationsRepository>()
            .AddScoped<IModelsRepository, ModelsRepository>()
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<ISuppliersRepository, SuppliersRepository>()
            .AddScoped<IUnitsRepository, UnitsRepository>()
            .AddScoped<IWarehousesRepository, WarehousesRepository>();
    }
}