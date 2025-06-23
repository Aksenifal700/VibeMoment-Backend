namespace VibeMoment.BusinessLogic.DTOs.Auth;

public record RegisterDto
{
    public string Email { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
}

