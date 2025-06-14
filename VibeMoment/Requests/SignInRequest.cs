using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Requests;

public class SignInRequest
{
    [Required]
    public string UsernameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}