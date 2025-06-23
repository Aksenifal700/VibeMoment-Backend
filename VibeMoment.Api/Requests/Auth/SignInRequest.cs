using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Api.Requests.Auth;

public record SignInRequest
{
    [Required]
    public string UsernameOrEmail { get; init; }

    [Required]
    public string Password { get; init; }

    public bool RememberMe { get; init; }
}