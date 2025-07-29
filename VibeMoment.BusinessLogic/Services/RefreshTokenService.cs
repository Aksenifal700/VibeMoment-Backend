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
    private readonly IAuthRepository _authRepository;
    
    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator, IAuthRepository authRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _authRepository = authRepository;
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
            throw new InvalidRefreshTokenException("Invalid refresh token");

        var email = await _authRepository.GetEmailAsync(tokenDto.UserId);
        
        if (string.IsNullOrEmpty(email))
            throw new UserNotFoundException();

        var jwt = _jwtTokenGenerator.GenerateToken(new TokenGenerationDto
        {
            UserId = tokenDto.UserId,
            Email = email,
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
            throw new InvalidRefreshTokenException("Token not found or already revoked");
        
        tokenDto.IsRevoked = true;
        await _refreshTokenRepository.RevokeAsync(token);
    }
}