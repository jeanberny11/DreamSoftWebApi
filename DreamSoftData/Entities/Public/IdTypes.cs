using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamSoftData.Entities.Base;

namespace DreamSoftData.Entities.Public
{
    public class IdTypes:IEntity<int>
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
