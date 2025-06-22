using AutoMapper;
using DreamSoftModel.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace DreamSoftModel.Config;

public static class DreamSoftModelServicesBuilder
{
    public static IServiceCollection AddModelProfiles(this IServiceCollection services)
    {
        return services.AddAutoMapperProfileServices();
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
}