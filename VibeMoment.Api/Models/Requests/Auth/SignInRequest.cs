using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Api.Models.Requests.Auth;

public class SignInRequest
{
    [Required]
    public string UsernameOrEmail { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
}


   
