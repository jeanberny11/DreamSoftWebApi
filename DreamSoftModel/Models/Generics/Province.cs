namespace DreamSoftModel.Models.Generics;

public class Province
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = null!;
    public Country Country { get; set; } = null!;
    public bool Active { get; set; }
}
