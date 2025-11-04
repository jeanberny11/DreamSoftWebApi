using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Menu;

[Table("menugroups")]
public class MenuGroups : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("menugroupid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MenuGroupId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }

    [Column("icon")] [MaxLength(50)] public string Icon { get; set; } = null!;

    [Column("sortorder")][MaxLength(50)] public int SortOrder { get; set; }

    [InverseProperty("MenuGroup")]
    public virtual ICollection<MenuOptions> MenuOptions { get; set; } = new HashSet<MenuOptions>();
}