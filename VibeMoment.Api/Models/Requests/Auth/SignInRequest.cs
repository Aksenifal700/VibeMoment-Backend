using System.ComponentModel.DataAnnotations;

namespace VibeMoment.Api.Models.Requests.Auth;

public class SignInRequest
{
    public string UsernameOrEmail { get; set; }
    
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
}


   
