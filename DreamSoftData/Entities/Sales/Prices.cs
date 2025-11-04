using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Sales;

[Table("prices", Schema = "sales")]
public class Prices : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("priceid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PriceId { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [InverseProperty("Price")]
    public virtual ICollection<Customers> Customers { get; set; } = new HashSet<Customers>();
}