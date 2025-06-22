using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("roleoptions")]
public class RoleOptions : IEntity<int>
{
    [Key]
    [Column("roleoptionid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleOptionId { get; set; }

    [Column("roleid")] public int RoleId { get; set; }

    [Column("menuoptionid")] public int MenuOptionId { get; set; }

    [Column("cancreate")] public bool CanCreate { get; set; }

    [Column("canread")] public bool CanRead { get; set; }

    [Column("canupdate")] public bool CanUpdate { get; set; }

    [Column("candelete")] public bool CanDelete { get; set; }

    // Foreign key navigation properties
    [ForeignKey("RoleId")] public Roles Role { get; set; } = null!;

    [ForeignKey("MenuOptionId")] public MenuOptions MenuOption { get; set; } = null!;
}