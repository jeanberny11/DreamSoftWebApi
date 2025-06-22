using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("accounttypes")]
public class AccountTypes : IEntity<int>
{
    [Key]
    [Column("accounttypeid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountTypeId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }
}