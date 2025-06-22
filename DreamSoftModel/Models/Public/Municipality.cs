namespace DreamSoftModel.Models.Public;

public class Municipality
{
    public int MunicipalityId { get; set; }
    public string Name { get; set; } = null!;
    public Province Province { get; set; } = null!;
    public bool Active { get; set; }
}