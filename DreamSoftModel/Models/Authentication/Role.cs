namespace DreamSoftModel.Models.Authentication;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public bool SuperUser { get; set; }
    public bool Admin { get; set; }
    public int AccountId { get; set; }
    public bool Active { get; set; }
}
