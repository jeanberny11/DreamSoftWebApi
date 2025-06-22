namespace DreamSoftModel.Models.Public;

public class MenuGroup
{
    public int MenuGroupId { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
    public string Icon { get; set; } = null!;
}