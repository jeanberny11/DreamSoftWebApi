using DreamSoftData.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamSoftData.Entities.Generics
{
    [Table("defaultvaluesetup")]
    public class DefaultValSetups:IEntity<int>
    {
        [Key]
        [Column("defaultvalueid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DefaultValueId { get; set; }

        [Column("newacctdefroleval")]
        public int NewAccountRoleId { get; set; }
    }
}
