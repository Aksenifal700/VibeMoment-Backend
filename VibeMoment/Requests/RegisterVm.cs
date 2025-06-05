using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Requests;

public class RegisterVm
{
    [Required]
    public string Username { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }
}