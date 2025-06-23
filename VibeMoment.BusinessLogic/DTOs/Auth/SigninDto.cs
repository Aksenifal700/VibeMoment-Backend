namespace VibeMoment.BusinessLogic.DTOs.Auth;

public record SigninDto
{
    public string UsernameOrEmail { get; init; }
    public string Password { get; init; }
}

   
