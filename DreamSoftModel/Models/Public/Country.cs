namespace DreamSoftModel.Models.Public;

public class Country
{
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
}