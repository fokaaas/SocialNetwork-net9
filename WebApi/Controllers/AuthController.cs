using Business.Interfaces;
using Business.Models.Auth;
using Business.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenModel>> SignUp([FromBody] SignUpModel signUpModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.SignUpAsync(signUpModel);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenModel>> SignIn([FromBody] SignInModel signInModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.SignInAsync(signInModel);
        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserModel>> Me()
    {
        var userId = HttpContext.Items["UserId"].ToString();
        var result = await _userService.GetByIdAsync(Convert.ToInt32(userId));
        return Ok(result);
    }
}