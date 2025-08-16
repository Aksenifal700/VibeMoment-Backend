using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IRefreshTokenService
{ 
    Task<RefreshTokenDto> GenerateAndSaveAsync(Guid userId);
    Task<SignInResultDto> RefreshJwtAsync(string refreshToken);
    Task RevokeAsync(string token);
}