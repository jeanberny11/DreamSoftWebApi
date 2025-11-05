using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Authentication;

[Table("passwordresettokens")]
public class PasswordResetTokens : IEntity<int>
{
    [Key]
    [Column("passwordresettokenid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PasswordResetTokenId { get; set; }

    [Column("tokenhash")]
    [MaxLength(256)]
    public string TokenHash { get; set; } = null!;

    [Column("userid")]
    public int UserId { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Column("expiresat")]
    public DateTime ExpiresAt { get; set; }

    [Column("isused")]
    public bool IsUsed { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    public virtual Users User { get; set; } = null!;

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;
}
