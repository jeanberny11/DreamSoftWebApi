using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("genders")]
public class Genders : IEntity<int>
{
    [Key]
    [Column("genderid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GenderId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }
}