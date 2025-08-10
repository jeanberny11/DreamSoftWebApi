namespace DreamSoftModel.Models.Inventory;

public class Product
{
    public int ProductId { get; set; }
    public int AccountId { get; set; }
    public string Reference { get; set; } = null!;
    public string Barcode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Brand Brand { get; set; } = null!;
    public Model Model { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Unit Unit { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public decimal LastCost { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal MaxDiscount { get; set; }
    public decimal ReorderPoint { get; set; }
    public bool SellWithoutStock { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
}