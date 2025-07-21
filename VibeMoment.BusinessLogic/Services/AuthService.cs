using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var isCreated = await _authRepository.CreateUserAsync(dto);
        
        return isCreated;
    }

    public async Task<string?> SignInAsync(SigninDto dto)
    {
        var userId = await  _authRepository.GetValidUserIdAsync(dto.UsernameOrEmail, dto.Password);
        
        if (userId is null)
            throw new UserNotFoundException("");

        var token = _jwtTokenGenerator.GenerateToken(new TokenGenerationDto
        {
            Email = dto.UsernameOrEmail,
            UserId = userId.Value
        });
        return token;
    }
}