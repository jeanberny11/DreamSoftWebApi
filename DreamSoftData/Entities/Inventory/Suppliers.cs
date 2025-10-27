using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Entities.Generics;

namespace DreamSoftData.Entities.Inventory;

[Table("suppliers", Schema = "inventory")]
public class Suppliers : IEntity<int>
{
    [Key]
    [Column("supplierid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SupplierId { get; set; }

    [Column("accountid")]
    public int AccountId { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Column("idnumber")]
    [MaxLength(50)]
    public string IdNumber { get; set; } = null!;

    [Column("idtypeid")]
    public int IdTypeId { get; set; }

    [Column("phonenumber")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Column("cellnumber")]
    [MaxLength(20)]
    public string CellNumber { get; set; } = null!;

    [Column("email")]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Column("address")]
    public string Address { get; set; } = null!;

    [Column("countryid")]
    public int CountryId { get; set; }

    [Column("provinceid")]
    public int ProvinceId { get; set; }

    [Column("municipalityid")]
    public int MunicipalityId { get; set; }

    [Column("creditlimit")]
    public decimal CreditLimit { get; set; }

    [Column("creditdays")]
    public int CreditDays { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("AccountId")]
    public virtual Accounts Account { get; set; } = null!;

    [ForeignKey("IdTypeId")]
    public virtual IdTypes IdType { get; set; } = null!;

    [ForeignKey("CountryId")]
    public virtual Countries Country { get; set; } = null!;

    [ForeignKey("ProvinceId")]
    public virtual Provinces Province { get; set; } = null!;

    [ForeignKey("MunicipalityId")]
    public virtual Municipalities Municipality { get; set; } = null!;
}