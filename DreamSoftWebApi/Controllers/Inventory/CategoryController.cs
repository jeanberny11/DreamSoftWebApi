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
[Permission("9", "Categorias")]
public class CategoryController(ICategoryServices services) : GenericControllerBase<Category, int>(services)
{
}