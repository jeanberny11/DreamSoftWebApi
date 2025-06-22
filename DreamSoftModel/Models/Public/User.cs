namespace DreamSoftModel.Models.Public;

public class User
{
    public int UserId { get; set; }
    public Account Account { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public bool Active { get; set; }
}