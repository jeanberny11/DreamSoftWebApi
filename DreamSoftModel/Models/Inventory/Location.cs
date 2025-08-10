using DreamSoftModel.Validations.Base;
using System.ComponentModel.DataAnnotations;

namespace DreamSoftModel.Models.Inventory;

public class Location
{
    public int LocationId { get; set; }
    [DescriptionRequired(50)]
    public string Name { get; set; } = null!;
    [Required]
    public int WarehouseId { get; set; }
    [AccountRequired]
    public int AccountId { get; set; }
    public bool Active { get; set; }
}