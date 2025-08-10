using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Public;

namespace DreamSoftData.Entities.Inventory;

[Table("brands", Schema = "inventory")]
public class Brands : IOSEntity<int>
{
    [Key]
    [Column("brandid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BrandId { get; set; }

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