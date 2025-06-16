using Microsoft.AspNetCore.Mvc;
using VibeMoment.Api.Responses;
using VibeMoment.Api.Requests;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var registerDto = _mapper.Map<RegisterUserDto>(request);
        
        var result = await _authService.RegisterAsync(registerDto);
        
        var response = _mapper.Map<AuthResponse>(result);
        
        return result.Success 
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("signin")]
    public async Task<ActionResult<AuthResponse>> SignIn([FromBody] SignInRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var loginDto = _mapper.Map<SigninUserDto>(request);
        
        var result = await _authService.SignInAsync(loginDto);
        
        var response = _mapper.Map<AuthResponse>(result);
        
        return result.Success 
            ? Ok(response)
            : Unauthorized(response);
    }

    [HttpPost("signout")]
    public async Task<ActionResult<AuthResponse>> SignOut()
    {
        var success = await _authService.SignOutAsync();
        
        var response = new AuthResponse
        {
            Success = success,
            Message = "Signed out successfully"
        };
        
        return Ok(response);
    }
}