namespace VibeMoment.BusinessLogic.DTOs.Auth;

public class TokenGenerationDto
{
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, object> CustomClaims { get; set; } = new();
}