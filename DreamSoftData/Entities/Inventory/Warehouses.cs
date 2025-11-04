using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Inventory;

[Table("warehouses", Schema = "inventory")]
public class Warehouses : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("warehouseid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WarehouseId { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;
}