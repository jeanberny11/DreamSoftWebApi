using AutoMapper;

namespace DreamSoftModel.Models.Converters;

public class DateOnlyToDateTimeConverter : ITypeConverter<DateOnly, DateTime>
{
    public DateTime Convert(DateOnly source, DateTime destination, ResolutionContext context)
    {
        return source.ToDateTime(TimeOnly.MinValue);
    }
}