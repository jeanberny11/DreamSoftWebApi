using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Generics;

[Table("provinces")]
public class Provinces : IEntity<int>
{
    [Key]
    [Column("provinceid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProvinceId { get; set; }

    [Column("name")] [MaxLength(100)] public string Name { get; set; } = null!;

    [Column("countryid")] public int CountryId { get; set; }

    [Column("active")] public bool Active { get; set; }

    [ForeignKey("CountryId")] public Countries Country { get; set; } = null!;


    [InverseProperty("Province")]
    public virtual ICollection<Municipalities> Municipalities { get; set; } = new HashSet<Municipalities>();

    [InverseProperty("Province")] public virtual ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
}