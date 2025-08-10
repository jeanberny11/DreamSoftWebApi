using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftModel.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamSoftModel.Profiles
{
    public class InventoryProfiles:Profile
    {
        public InventoryProfiles()
        {
            CreateMap<Brands, Brand>().ReverseMap();
            CreateMap<Categories, Category>().ReverseMap();
            CreateMap<Locations, Location>().ReverseMap();
            CreateMap<DreamSoftData.Entities.Inventory.Models, Model>().ReverseMap();
            CreateMap<Products, Product>().ReverseMap()
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Brand.BrandId))
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => src.Model.ModelId))
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId))
            .ForMember(dest => dest.Unit, opt => opt.Ignore())
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.UnitId))
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Location.LocationId));           
            CreateMap<Suppliers, Supplier>().ReverseMap();
            CreateMap<Units, Unit>().ReverseMap();
            CreateMap<Warehouses, Warehouse>().ReverseMap();
        }
    }
}
