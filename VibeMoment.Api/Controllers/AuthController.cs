using Microsoft.AspNetCore.Mvc;
using VibeMoment.Api.Responses;
using VibeMoment.Api.Requests.Auth;
using VibeMoment.BusinessLogic.Services.Interfaces;
using VibeMoment.BusinessLogic.DTOs;
using AutoMapper;


namespace VibeMoment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        var registerDto = _mapper.Map<RegisterDto>(request);
        
        var success = await _authService.RegisterAsync(registerDto);
        
        return success 
            ? Ok()
            : BadRequest();
    }

    [HttpPost("signin")]
    public async Task<ActionResult<AuthResponse>> SignIn([FromBody] SignInRequest request)
    {
        var loginDto = _mapper.Map<SigninDto>(request);
        
        var success = await _authService.SignInAsync(loginDto);
        
        return success 
            ? Ok()
            : Unauthorized();
    }

    [HttpPost("signout")]
    public async Task<ActionResult<AuthResponse>> SignOut()
    {
        var isSucces = await _authService.SignOutAsync();
        
        if (isSucces)
        {
            return Ok();
        }
        return BadRequest();
    }
}