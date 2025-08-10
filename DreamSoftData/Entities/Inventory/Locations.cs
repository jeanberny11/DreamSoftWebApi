using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Public;

namespace DreamSoftData.Entities.Inventory;

[Table("locations", Schema = "inventory")]
public class Locations : IEntity<int>
{
    [Key]
    [Column("locationid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocationId { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Column("warehouseid")]
    public int WarehouseId { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [ForeignKey("WarehouseId")]
    public virtual Warehouses? Warehouse { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;
}