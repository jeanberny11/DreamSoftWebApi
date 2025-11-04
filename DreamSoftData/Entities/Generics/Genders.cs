using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Generics;

[Table("genders")]
public class Genders : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("genderid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GenderId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }
}