using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using VibeMoment.Api.Models.Requests;
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
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(IAuthService authService, IMapper mapper, IRefreshTokenService refreshTokenService)
    {
        _authService = authService;
        _mapper = mapper;
        _refreshTokenService = refreshTokenService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var registerDto = _mapper.Map<RegisterDto>(request);

        await _authService.RegisterAsync(registerDto);

        return Ok();
    }
    
    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
    {
        var signinDto = _mapper.Map<SigninDto>(request);
        var result = await _authService.SignInAsync(signinDto);

        var response = new SignInResponse
        {
            Token = result.Token,
            RefreshToken = result.RefreshToken
        };
        
        return Ok(response);
    }

    [HttpPost("token/refresh")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshJwtAsync(request.RefreshToken);
        
        return Ok(new RefreshTokenResponse
        {
            Token = result.Token,
            RefreshToken = result.RefreshToken
        });
    }
    
    [HttpPost("token/revoke")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
    {
        await _refreshTokenService.RevokeAsync(request.RefreshToken);
        return Ok();
    }
}