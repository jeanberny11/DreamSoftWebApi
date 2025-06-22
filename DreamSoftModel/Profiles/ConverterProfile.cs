using AutoMapper;
using DreamSoftModel.Models.Converters;

namespace DreamSoftModel.Profiles;

public class ConverterProfile : Profile
{
    public ConverterProfile()
    {
        CreateMap<DateOnly, DateTime>().ConvertUsing(new DateOnlyToDateTimeConverter());
        CreateMap<DateTime, DateOnly>().ConvertUsing(new DateTimeToDateOnlyConverter());
    }
}