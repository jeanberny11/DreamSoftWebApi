namespace DreamSoftModel.Models.Public;

public class Account
{
    public int AccountId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Country Country { get; set; } = null!;
    public Province Province { get; set; } = null!;
    public Municipality Municipality { get; set; } = null!;
    public AccountType AccountType { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public Gender Gender { get; set; } = null!;
    public bool Active { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string IdNumber { get; set; } = null!;
    public IdType IdType { get; set; } = null!;
}