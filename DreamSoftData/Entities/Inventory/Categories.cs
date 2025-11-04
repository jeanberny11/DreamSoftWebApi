using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Inventory;

[Table("categories", Schema = "inventory")]
public class Categories : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("categoryid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

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