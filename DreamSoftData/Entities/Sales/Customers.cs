using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Public;

namespace DreamSoftData.Entities.Sales;

[Table("customers", Schema = "sales")]
public class Customers : IEntity<int>
{
    [Key]
    [Column("customerid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    [Column("firstname")]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Column("lastname")]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Column("address")]
    [MaxLength(500)]
    public string Address { get; set; } = null!;

    [Column("phone")]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    [Column("cellular")]
    [MaxLength(20)]
    public string Cellular { get; set; } = null!;

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("idnumber")]
    [MaxLength(50)]
    public string IdNumber { get; set; } = null!;

    [Column("idtypeid")]
    public int IdTypeId { get; set; }

    [Column("dob")]
    public DateTime? Dob { get; set; }

    [Column("nickname")]
    [MaxLength(100)]
    public string? Nickname { get; set; }

    [Column("genderid")]
    public int GenderId { get; set; }

    [Column("countyid")]
    public int CountyId { get; set; }

    [Column("provinceid")]
    public int ProvinceId { get; set; }

    [Column("municipalityid")]
    public int MunicipalityId { get; set; }

    [Column("creditlimit")]
    public decimal CreditLimit { get; set; }

    [Column("priceid")]
    public int PriceId { get; set; }

    [Column("maritalstatusid")]
    public int MaritalStatusId { get; set; }

    [Column("creditdays")]
    public int CreditDays { get; set; }

    [Column("taxtypeid")]
    public int TaxTypeId { get; set; }

    [Column("taxnumber")]
    [MaxLength(50)]
    public string TaxNumber { get; set; } = null!;

    [Column("exempttaxes")]
    public bool ExemptTaxes { get; set; }

    [Column("maxdiscount")]
    public decimal? MaxDiscount { get; set; }

    // Navigation properties
    [ForeignKey("CountyId")]
    public virtual Countries County { get; set; } = null!;

    [ForeignKey("ProvinceId")]
    public virtual Provinces Province { get; set; } = null!;

    [ForeignKey("MunicipalityId")]
    public virtual Municipalities Municipality { get; set; } = null!;

    [ForeignKey("IdTypeId")]
    public virtual IdTypes IdType { get; set; } = null!;

    [ForeignKey("GenderId")]
    public virtual Genders Gender { get; set; } = null!;

    [ForeignKey("PriceId")]
    public virtual Prices Price { get; set; } = null!;

    [ForeignKey("MaritalStatusId")]
    public virtual MaritalStatus MaritalStatus { get; set; } = null!;

    [ForeignKey("TaxTypeId")]
    public virtual TaxType TaxType { get; set; } = null!;
}