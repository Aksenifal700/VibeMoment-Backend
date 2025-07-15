namespace VibeMoment.Api.Models.Requests.Auth;

public class TokenGenerationRequest
{
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, string> CustomClaims { get; set; } = new();

}