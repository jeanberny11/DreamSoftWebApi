using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Public;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class AccountController(IAccountServices services):ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<Account>> CreateAccount([FromBody] AccountCreate account)
    {
        var result = await services.CreateNewAccount(account);
            return result;
    }
}