using DreamSoftLogic.Services.Public.Impl;
using DreamSoftLogic.Services.Public.Interface;
using DreamSoftLogic.Services.SecurityConfig.Impl;
using DreamSoftLogic.Services.SecurityConfig.Interface;
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
            .AddScoped<ITokenServices, TokenServices>();
    }
}