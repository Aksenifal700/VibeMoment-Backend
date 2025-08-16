using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshTokenDto> CreateAsync(CreateRefreshTokenDto dto);
    Task<RefreshTokenDto?> GetRefreshTokenAsync(string token);
    Task RevokeAsync(string token);
}