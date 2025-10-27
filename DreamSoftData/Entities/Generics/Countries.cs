using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Generics;

[Table("countries")]
public class Countries : IEntity<int>
{
    [Key]
    [Column("countryid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountryId { get; set; }

    [Column("name")] [MaxLength(50)] public string Name { get; set; } = null!;

    [Column("active")] public bool Active { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<Provinces> Provinces { get; set; } = new HashSet<Provinces>();

    [InverseProperty("Country")] public virtual ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
}