using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Inventory;

[Table("units", Schema = "inventory")]
public class Units : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("unitid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UnitId { get; set; }

    [Column("name")]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    [Column("abbreviation")]
    [MaxLength(10)]
    public string Abbreviation { get; set; } = null!;

    [Column("allow_fractions")]
    public bool AllowFractions { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;
}