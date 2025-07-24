using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces;

public interface IJwtTokenGenerator
{
    public string? GenerateToken(TokenGenerationDto dto);
}