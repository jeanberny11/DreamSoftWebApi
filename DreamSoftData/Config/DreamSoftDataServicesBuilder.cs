using DreamSoftData.Repositories.Public.Impl;
using DreamSoftData.Repositories.Public.Interface;
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
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IDefaultValSetupsRepository,DefaultValSetupRepository>()
            .AddScoped<IAccountTypesRepository,AccountTypesRepository>();
    }
}