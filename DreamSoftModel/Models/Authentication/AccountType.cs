namespace DreamSoftModel.Models.Authentication;

public class AccountType
{
    public int AccountTypeId { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
}
