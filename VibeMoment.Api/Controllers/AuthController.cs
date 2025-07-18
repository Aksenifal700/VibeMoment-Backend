using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using VibeMoment.Api.Models.Requests.Auth;
using VibeMoment.Api.Models.Responses;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Services;

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
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var registerDto = _mapper.Map<RegisterDto>(request);

        await _authService.RegisterAsync(registerDto);

        return Ok();
    }
    
    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
    {
        var loginDto = _mapper.Map<SigninDto>(request);
        var result = await _authService.SignInAsync(loginDto);
        var response = _mapper.Map<SignInResponse>(result);
        
        return Ok(response);
    }
}