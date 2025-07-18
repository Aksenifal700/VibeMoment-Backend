using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic;

public interface IJwtTokenGenerator
{
    public TokenResultDto GenerateToken(TokenGenerationDto dto);
}