using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Generics;

[Table("municipalities")]
public class Municipalities : IEntity<int>
{
    [Key]
    [Column("municipalityid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MunicipalityId { get; set; }

    [Column("name")] [MaxLength(100)] public string Name { get; set; } = null!;

    [Column("provinceid")] public int ProvinceId { get; set; }

    [Column("active")] public bool Active { get; set; }

    [ForeignKey("ProvinceId")] public Provinces Province { get; set; } = null!;


    [InverseProperty("Municipality")]
    public virtual ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
}