namespace DreamSoftModel.Models.Public;

public class MenuOption
{
    public int MenuOptionId { get; set; }
    public string Name { get; set; } = null!;
    public Module Module { get; set; } = null!;
    public MenuGroup MenuGroup { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool Active { get; set; }
}