using DreamSoftModel.Validations.Base;

namespace DreamSoftModel.Models.Inventory;

public class Warehouse
{
    public int WarehouseId { get; set; }
    [DescriptionRequired(50)]
    public string Name { get; set; } = null!;
    [AccountRequired]
    public int AccountId { get; set; }
    public bool Active { get; set; }
}