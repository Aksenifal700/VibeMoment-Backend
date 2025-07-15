using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using VibeMoment.Api.Models.Requests.Auth;
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

        var isSuccess = await _authService.RegisterAsync(registerDto);

        return isSuccess
            ? Ok()
            : ValidationProblem(ModelState);
    }
    
    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
    {
        var loginDto = _mapper.Map<SigninDto>(request);

        var token = await _authService.SignInAsync(loginDto);

        return token is not null 
            ? Ok(token)
            : ValidationProblem(ModelState);
    }

    [HttpPost("signout")]
    public async Task<ActionResult> SignOut()
    {
        var isSuccess = await _authService.SignOutAsync();

        return isSuccess
            ? Ok()
            : BadRequest();

    }
}