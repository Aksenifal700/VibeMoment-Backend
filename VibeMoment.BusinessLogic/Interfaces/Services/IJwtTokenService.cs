using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(TokenGenerationDto dto);
}