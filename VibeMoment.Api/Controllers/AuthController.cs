using Microsoft.AspNetCore.Mvc;
using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Services.Interfaces;

namespace VibeMoment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message, errors = result.Errors });
        }

        return Ok(new { message = result.Message, userId = result.UserId });
    }

    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
    {
        var result = await _authService.SignInAsync(request);
        
        if (!result.Success)
        {
            return Unauthorized(new { message = result.Message });
        }

        return Ok(new { message = result.Message, userId = result.UserId });
    }

    [HttpPost("signout")]
    public async Task<ActionResult> SignOut()
    {
        await _authService.SignOutAsync();
        return Ok(new { message = "Signed out successfully" });
    }
}