using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("taxtype")]
public class TaxType : IEntity<int>
{
    [Key]
    [Column("taxtypeid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaxTypeId { get; set; }

    [Column("name")] public string Name { get; set; } = null!;
    [Column("active")] public bool Active { get; set; }
}