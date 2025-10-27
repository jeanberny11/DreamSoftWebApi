using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Menu;

[Table("menuoptions")]
public class MenuOptions : IEntity<int>
{
    [Key]
    [Column("menuoptionid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MenuOptionId { get; set; }

    [Column("moduleid")] public int ModuleId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }

    [Column("url")] [MaxLength(100)] public string Url { get; set; } = null!;

    [Column("icon")] [MaxLength(50)] public string Icon { get; set; } = null!;

    [Column("menugroupid")] public int MenuGroupId { get; set; }

    [Column("sortorder")] public int SortOrder { get; set; }

    [ForeignKey("ModuleId")] public Modules Module { get; set; } = null!;

    [ForeignKey("MenuGroupId")] public MenuGroups MenuGroup { get; set; } = null!;

    [InverseProperty("MenuOption")]
    public virtual ICollection<RoleOptions> RoleOptions { get; set; } = new HashSet<RoleOptions>();
}