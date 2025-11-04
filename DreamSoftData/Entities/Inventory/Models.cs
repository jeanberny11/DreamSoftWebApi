using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Inventory;

[Table("models", Schema = "inventory")]
public class Models : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("modelid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ModelId { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Column("brandid")]
    public int BrandId { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [ForeignKey("BrandId")]
    public virtual Brands? Brand { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;
}