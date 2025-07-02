using AutoMapper;
using DreamSoftModel.Profiles;
using DreamSoftModel.Validations.Public;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace DreamSoftModel.Config;

public static class DreamSoftModelServicesBuilder
{
    public static IServiceCollection AddModelProfiles(this IServiceCollection services)
    {
        return services.AddAutoMapperProfileServices()
            .AddValidatorServices();
    }

    public static IServiceCollection AddAutoMapperProfileServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<ConverterProfile>();
            mc.AddProfile<PublicProfile>();
        });
        var mapper = mapperConfig.CreateMapper();
        return services.AddSingleton(mapper);
    }

    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<AccountValidator>();
    }
}