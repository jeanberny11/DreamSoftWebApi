using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Authentication;

[Table("users")]
public class Users : IEntity<int>
{
    [Key]
    [Column("userid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column("accountid")] public int AccountId { get; set; }

    [Column("firstname")] [MaxLength(100)] public string FirstName { get; set; } = null!;

    [Column("lastname")] [MaxLength(100)] public string LastName { get; set; } = null!;

    [Column("username")] [MaxLength(50)] public string UserName { get; set; } = null!;

    [Column("password")] [MaxLength(50)] public string Password { get; set; } = null!;

    [Column("roleid")] public int RoleId { get; set; }

    [Column("active")] public bool Active { get; set; }

    [ForeignKey("AccountId")] public Accounts Account { get; set; } = null!;

    [ForeignKey("RoleId")] public Roles Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<RefreshTokens> RefreshTokens { get; set; } = new HashSet<RefreshTokens>();
}