namespace DreamSoftModel.Models.Inventory;

public class Supplier
{
    public int SupplierId { get; set; }
    public int AccountId { get; set; }
    public string Name { get; set; } = null!;
    public string IdNumber { get; set; } = null!;
    public int IdTypeId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string CellNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
    public int MunicipalityId { get; set; }
    public decimal CreditLimit { get; set; }
    public int CreditDays { get; set; }
    public bool Active { get; set; }
    public DateTime? CreatedAt { get; set; }
}