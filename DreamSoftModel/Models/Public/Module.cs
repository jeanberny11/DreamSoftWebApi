namespace DreamSoftModel.Models.Public;

public class Module
{
    public int ModuleId { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
    public string Icon { get; set; } = null!;
    public int SortOrder { get; set; }
}