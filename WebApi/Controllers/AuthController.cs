using Business.Interfaces;
using Business.Models.Auth;
using Business.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("sign-up")]
    public async Task<ActionResult<TokenModel>> SignUp([FromBody] SignUpModel signUpModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.SignUpAsync(signUpModel);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<TokenModel>> SignIn([FromBody] SignInModel signInModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.SignInAsync(signInModel);
        return Ok(result);
    }
    
    [HttpPost("me")]
    [AllowAnonymous]
    public async Task<ActionResult<UserModel>> Me()
    {
        var userId = HttpContext.Items["UserId"].ToString();
        var result = await _authService.GetCurrentUserAsync(Convert.ToInt32(userId));
        return Ok(result);
    }
}