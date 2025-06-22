using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Public;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
public class AccountController
{
}