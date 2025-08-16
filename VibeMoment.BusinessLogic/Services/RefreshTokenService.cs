using System.Security.Cryptography;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<RefreshTokenDto> GenerateAndSaveAsync(Guid userId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var expiresOn = DateTime.UtcNow.AddDays(7);

        var createDto = new CreateRefreshTokenDto
        {
            UserId = userId,
            Token = token,
            ExpiresOnUtc = expiresOn
        };
        
        return await _refreshTokenRepository.CreateAsync(createDto);
    }

    public async Task<SignInResultDto> RefreshJwtAsync(string refreshToken)
    {
        var tokenDto = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
        
        if (tokenDto is null || tokenDto.ExpiresOnUtc < DateTime.UtcNow || tokenDto.IsRevoked)
            throw InvalidRefreshTokenException.Invalid();

        var userDto = await _userRepository.GetByIdAsync(tokenDto.UserId);
        
        if (userDto is null)
            throw new UserNotFoundException();

        var jwt = _jwtTokenGenerator.GenerateToken(new TokenGenerationDto
        {
            UserId = tokenDto.UserId,
            Email = userDto.Email,
        });

        return new SignInResultDto()
        {
            Token = jwt,
            RefreshToken = tokenDto.Token,
        };
    }
    
    public async Task RevokeAsync(string token)
    {
        var tokenDto = await _refreshTokenRepository.GetRefreshTokenAsync(token);
        
        if (tokenDto is null || tokenDto.IsRevoked)
            throw InvalidRefreshTokenException.RevokedOrMissing();
        
        tokenDto.IsRevoked = true;
        await _refreshTokenRepository.RevokeAsync(token);
    }
}