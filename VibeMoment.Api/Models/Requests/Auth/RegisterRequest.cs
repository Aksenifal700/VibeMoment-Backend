using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Api.Models.Requests.Auth;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required, MinLength(6)]
    public string Password { get; set; }
    
}








  