using VibeMoment.Requests;
using VibeMoment.Results;

namespace VibeMoment.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest register);
    Task<AuthResult> SignInAsync(SignInRequest signIn);
    Task SignOutAsync();
}

