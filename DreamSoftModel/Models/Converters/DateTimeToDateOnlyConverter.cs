using AutoMapper;

namespace DreamSoftModel.Models.Converters;

public class DateTimeToDateOnlyConverter : ITypeConverter<DateTime, DateOnly>
{
    public DateOnly Convert(DateTime source, DateOnly destination, ResolutionContext context)
    {
        return DateOnly.FromDateTime(source);
    }
}