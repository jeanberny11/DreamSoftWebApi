using AutoMapper;
using DreamSoftData.Entities.Public;
using DreamSoftModel.Models.Public;

namespace DreamSoftModel.Profiles;

public class PublicProfile : Profile
{
    public PublicProfile()
    {
        CreateMap<Accounts, Account>().ReverseMap();
        CreateMap<Country, Country>().ReverseMap();
        CreateMap<Province, Province>().ReverseMap();
        CreateMap<Municipality, Municipality>().ReverseMap();
        CreateMap<AccountType, AccountType>().ReverseMap();
        CreateMap<MenuOptions, MenuOption>().ReverseMap();
        CreateMap<Users, User>().ReverseMap();
        CreateMap<Roles, Role>().ReverseMap();
        CreateMap<Genders, Gender>().ReverseMap();
        CreateMap<Modules, Module>().ReverseMap();
        CreateMap<RoleOptions, RoleOption>().ReverseMap();
        CreateMap<MenuGroups, MenuGroup>().ReverseMap();
    }
}