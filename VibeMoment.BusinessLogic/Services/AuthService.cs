using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IAuthRepository authRepository, IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        return await _authRepository.CreateUserAsync(
            dto.Email,
            dto.Password,
            dto.UserName);
    }

    public async Task<string?> SignInAsync(SigninDto dto)
    {
        var userId = await  _authRepository.GetValidUserIdAsync(dto.UsernameOrEmail, dto.Password);
        
        if (userId is null)
            return null;

        var token = _jwtTokenService.GenerateToken(new TokenGenerationDto
        {
            Email = dto.UsernameOrEmail,
            UserId = userId.Value
        });
        return token;
    }

    public async Task<bool> SignOutAsync()
    {
        await _authRepository.SignOutAsync();
        return true;
    }
}