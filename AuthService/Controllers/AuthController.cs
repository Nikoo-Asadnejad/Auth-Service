using AuthService.Dtos.User;
using AuthService.Interfaces;
using AuthService.Utils;
using ErrorHandlingDll.ReturnTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    /// <summary>
    /// Sign in request
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    [HttpPost("api/sign-in/v1")]
    [ProducesResponseType(typeof(ReturnModel<long>),200)]
    [ProducesResponseType(typeof(ReturnModel<long>), 400)]
    [ProducesResponseType(typeof(ReturnModel<long>), 404)]
    [ProducesResponseType(typeof(ReturnModel<long>), 500)]
    public async Task<IActionResult> SignIn([FromBody]SignInDto signInDto)
    {
      
      var result =await _authService.SignIn(signInDto);
      return StatusCode((int)result.HttpStatusCode,result);
    }

    /// <summary>
    /// Sign up request
    /// </summary>
    /// <param name="signUpDto"></param>
    /// <returns></returns>

    [HttpPost("api/sign-up/v1")]
    [ProducesResponseType(typeof(ReturnModel<long>), 200)]
    [ProducesResponseType(typeof(ReturnModel<long>), 400)]
    [ProducesResponseType(typeof(ReturnModel<long>), 500)]
    public async Task<IActionResult> SignUp(SignUpDto signUpDto)
    {
      var result = await _authService.SignUp(signUpDto);
      return StatusCode((int)result.HttpStatusCode, result);
    }

    /// <summary>
    /// Generates a token for authecticating user
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("api/generate-token/v1")]
    [ProducesResponseType(typeof(ReturnModel<long>), 200)]
    [ProducesResponseType(typeof(ReturnModel<long>), 400)]
    [ProducesResponseType(typeof(ReturnModel<long>), 404)]
    [ProducesResponseType(typeof(ReturnModel<long>), 500)]
    public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenDto input)
    {
      var result = await _authService.GenerateToken(input);
      return StatusCode((int)result.HttpStatusCode, result);
    }
  }
}
