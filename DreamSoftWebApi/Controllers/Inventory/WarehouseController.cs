using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Inventory;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("13", "Almacenes")]
public class WarehouseController(IWarehouseServices services) : GenericControllerBase<Warehouse, int>(services)
{
}