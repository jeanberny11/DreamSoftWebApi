using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Menu;

namespace DreamSoftData.Entities.Authentication;

[Table("roles")]
public class Roles : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("roleid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    [Column("name")] [MaxLength(100)] public string Name { get; set; } = null!;

    [Column("superuser")] public bool SuperUser { get; set; }

    [Column("admin")] public bool Admin { get; set; }

    [Column("accountid")] public int AccountId { get; set; }

    [Column("active")] public bool Active { get; set; }

    [ForeignKey("AccountId")] public Accounts Account { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<RoleOptions> RoleOptions { get; set; } = new HashSet<RoleOptions>();

    [InverseProperty("Role")] public virtual ICollection<Users> Users { get; set; } = new HashSet<Users>();
}