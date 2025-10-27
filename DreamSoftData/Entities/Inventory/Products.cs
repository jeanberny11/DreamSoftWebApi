using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Inventory;

[Table("products", Schema = "inventory")]
public class Products : IEntity<int>
{
    [Key]
    [Column("productid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("reference")]
    [MaxLength(50)]
    public string Reference { get; set; } = null!;

    [Column("barcode")]
    [MaxLength(50)]
    public string? Barcode { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("brandid")]
    public int BrandId { get; set; }

    [Column("modelid")]
    public int ModelId { get; set; }

    [Column("categoryid")]
    public int CategoryId { get; set; }

    [Column("unitid")]
    public int UnitId { get; set; }

    [Column("locationid")]
    public int LocationId { get; set; }

    [Column("lastcost")]
    public decimal LastCost { get; set; }

    [Column("tax_percent")]
    public decimal TaxPercent { get; set; }

    [Column("maxdiscount")]
    public decimal MaxDiscount { get; set; }

    [Column("reorder_point")]
    public decimal ReorderPoint { get; set; }

    [Column("sell_without_stock")]
    public bool SellWithoutStock { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;

    [ForeignKey("BrandId")]
    public virtual Brands? Brand { get; set; }

    [ForeignKey("ModelId")]
    public virtual Models? Model { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Categories? Category { get; set; }

    [ForeignKey("UnitId")]
    public virtual Units Unit { get; set; } = null!;

    [ForeignKey("LocationId")]
    public virtual Locations? Location { get; set; }
}