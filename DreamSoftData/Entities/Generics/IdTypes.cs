using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;
using DreamSoftData.Entities.Authentication;

namespace DreamSoftData.Entities.Generics
{
    [Table("idtypes")]
    public class IdTypes : IEntity<int>, IActiveEntity
    {
        [Key]
        [Column("idtypeid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTypeId { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Column("active")]
        public bool Active { get; set; }

        [InverseProperty("IdType")] public virtual ICollection<Accounts> Accounts { get; set; } = new HashSet<Accounts>();
    }
}
