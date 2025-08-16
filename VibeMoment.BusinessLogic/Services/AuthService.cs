using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenService refreshTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var isCreated = await _userRepository.CreateUserAsync(dto);
        
        return isCreated;
    }

    public async Task<SignInResultDto> SignInAsync(SigninDto dto)
    {
        var userId = await  _userRepository.GetValidUserIdAsync(dto.UsernameOrEmail, dto.Password);
        
        if (userId is null)
            throw new UserNotFoundException();

        var jwtToken = _jwtTokenGenerator.GenerateToken(new TokenGenerationDto
        {
            Email = dto.UsernameOrEmail,
            UserId = userId.Value
        });

        var refreshToken = await _refreshTokenService.GenerateAndSaveAsync(userId.Value);

        return new SignInResultDto
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }
    
    public async Task<SignInResultDto> RefreshJwtAsync(string refreshToken)
    {
        return await _refreshTokenService.RefreshJwtAsync(refreshToken);
    }
}