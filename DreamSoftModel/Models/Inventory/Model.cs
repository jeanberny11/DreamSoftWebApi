using DreamSoftModel.Validations.Base;
using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Models.Inventory;

public class Model
{
    public int ModelId { get; set; }
    [DescriptionRequired(50)]
    public string Name { get; set; } = null!;
    [Required]
    public int BrandId { get; set; }
    [AccountRequired]
    public int AccountId { get; set; }
    public bool Active { get; set; }
}