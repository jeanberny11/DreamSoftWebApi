using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public;

[Table("refreshtokens")]
public class RefreshTokens : IEntity<int>
{
    [Key]
    [Column("refreshtokenid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RefreshTokenId { get; set; }

    [Column("userid")] public int UserId { get; set; }

    [Column("token")] [MaxLength(900)] public string Token { get; set; } = null!;

    [Column("expiresat")] public DateTime ExpiresAt { get; set; }

    [Column("createdat")] public DateTime CreatedAt { get; set; }

    [Column("createdbyip")]
    [MaxLength(50)]
    public string? CreatedByIp { get; set; }

    [Column("revokedat")] public DateTime? RevokedAt { get; set; }

    [Column("revokedbyip")]
    [MaxLength(50)]
    public string? RevokedByIp { get; set; }

    [ForeignKey("UserId")] public virtual Users User { get; set; } = null!;
}