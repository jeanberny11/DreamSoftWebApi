using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Entities.Generics;
using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.Menu;
using DreamSoftModel.Models.Generics;

namespace DreamSoftModel.Profiles;

public class PublicProfile : Profile
{
    public PublicProfile()
    {
        CreateMap<Accounts, Account>().ReverseMap();
        CreateMap<Countries, Country>().ReverseMap();
        CreateMap<Provinces, Province>().ReverseMap();
        CreateMap<Municipalities, Municipality>().ReverseMap();
        CreateMap<AccountTypes, AccountType>().ReverseMap();
        CreateMap<MenuOptions, MenuOption>().ReverseMap()
            .ForMember(dest => dest.Module, opt => opt.Ignore())
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
            .ForMember(dest => dest.MenuGroup , opt => opt.Ignore())
            .ForMember(dest => dest.MenuGroupId, opt => opt.MapFrom(src => src.MenuGroup.MenuGroupId));
        CreateMap<Users, User>().ReverseMap()
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.RoleId))
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.AccountId));
        CreateMap<Roles, Role>().ReverseMap()
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId));
        CreateMap<Genders, Gender>().ReverseMap();
        CreateMap<Modules, Module>().ReverseMap();
        CreateMap<RoleOptions, RoleOption>().ReverseMap()
            .ForMember(dest => dest.MenuOption, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.MenuOptionId, opt => opt.MapFrom(src => src.MenuOption.MenuOptionId))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.RoleId));
        CreateMap<MenuGroups, MenuGroup>().ReverseMap();
        CreateMap<IdTypes, IdType>().ReverseMap();
        CreateMap<AccountTypes, AccountType>().ReverseMap();
    }
}