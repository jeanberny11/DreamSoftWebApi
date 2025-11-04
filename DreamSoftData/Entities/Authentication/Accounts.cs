using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Generics;

namespace DreamSoftData.Entities.Authentication;

[Table("accounts")]
public class Accounts : IEntity<int>, IActiveEntity
{
    [Key]
    [Column("accountid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }

    [Column("firstname")] [MaxLength(100)] public string FirstName { get; set; } = null!;

    [Column("lastname")] [MaxLength(100)] public string LastName { get; set; } = null!;

    [Column("phone")] [MaxLength(15)] public string Phone { get; set; } = null!;

    [Column("email")] [MaxLength(100)] public string Email { get; set; } = null!;

    [Column("address")] [MaxLength(150)] public string Address { get; set; } = null!;

    [Column("countryid")] public int CountryId { get; set; }

    [Column("provinceid")] public int ProvinceId { get; set; }

    [Column("municipalityid")] public int MunicipalityId { get; set; }

    [Column("accounttypeid")] public int AccountTypeId { get; set; }

    [Column("dob")] public DateTime? Dob { get; set; }

    [Column("genderid")] public int GenderId { get; set; }

    [Column("active")] public bool Active { get; set; }

    [Column("cdate")] public DateTime CDate { get; set; }

    [Column("emailverified")] public bool EmailVerified { get; set; }

    [Column("phoneverified")] public bool PhoneVerified { get; set; }

    [Column("idverified")] public bool IdVerified { get; set; }

    [Column("accountnumber")]
    [MaxLength(20)]
    public string AccountNumber { get; set; } = null!;
    [Column("idnumber")]
    [MaxLength(50)]
    public string IdNumber { get; set; } = null!;
    [Column("idtypeid")] public int IdTypeId { get; set; }

    [ForeignKey("CountryId")] public Countries Country { get; set; } = null!;

    [ForeignKey("ProvinceId")] public Provinces Province { get; set; } = null!;

    [ForeignKey("MunicipalityId")] public Municipalities Municipality { get; set; } = null!;

    [ForeignKey("AccountTypeId")] public AccountTypes AccountType { get; set; } = null!;

    [ForeignKey("GenderId")] public Genders Gender { get; set; } = null!;
    [ForeignKey("IdTypeId")] public IdTypes IdType { get; set; } = null!;

    [InverseProperty("Account")] public virtual ICollection<Roles> Roles { get; set; } = new HashSet<Roles>();

    [InverseProperty("Account")] public virtual ICollection<Users> Users { get; set; } = new HashSet<Users>();

}