using DreamSoftModel.Validations.Base;
using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Models.Inventory;

public class Unit
{
    public int UnitId { get; set; }
    [DescriptionRequired(50)]
    public string Name { get; set; } = null!;
    [Required]
    public string Abbreviation { get; set; } = null!;
    public bool AllowFractions { get; set; }
    [AccountRequired]
    public int AccountId { get; set; }
    public bool Active { get; set; }
}