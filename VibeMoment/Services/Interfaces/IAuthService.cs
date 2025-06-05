using Microsoft.AspNetCore.Identity;
using VibeMoment.Requests;

namespace VibeMoment.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterVm register);
    Task<AuthResult> SignInAsync(SignInVm signIn);
    Task SignOutAsync();
}

public class AuthResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? UserId { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
}