namespace DreamSoftModel.Models.Public;

public class RoleOption
{
    public int RoleOptionId { get; set; }
    public Role Role { get; set; } = null!;
    public MenuOption MenuOption { get; set; } = null!;
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}