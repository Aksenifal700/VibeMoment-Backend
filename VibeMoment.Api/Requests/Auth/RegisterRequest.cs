using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Api.Requests.Auth;

public record RegisterRequest
{
    [Required]
    public string Username { get; init; }

    [Required, EmailAddress]
    public string Email { get; init; }

    [Required, MinLength(6)]
    public string Password { get; init; }
}


  