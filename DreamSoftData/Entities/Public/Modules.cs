using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("modules")]
public class Modules : IEntity<int>
{
    [Key]
    [Column("moduleid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ModuleId { get; set; }

    [Column("name")] [MaxLength(100)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }

    [Column("icon")] [MaxLength(50)] public string Icon { get; set; } = null!;

    [InverseProperty("Module")]
    public virtual ICollection<MenuOptions> MenuOptions { get; set; } = new HashSet<MenuOptions>();
}