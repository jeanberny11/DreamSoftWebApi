using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Generics;

[Table("maritalstatus")]
public class MaritalStatus : IEntity<int>
{
    [Key]
    [Column("maritalstatusid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaritalStatusId { get; set; }

    [Column("name")] public string Name { get; set; } = null!;
    [Column("active")] public bool Active { get; set; }
}