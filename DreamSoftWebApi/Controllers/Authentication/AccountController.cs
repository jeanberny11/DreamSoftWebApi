using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Authentication;

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
