using AuthService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [Authorized()]
    public async Task<IActionResult> SignIn()
    {

      return Ok();
    }

    public async Task<IActionResult> SignUp()
    {
      return Ok();
    }
  }
}
