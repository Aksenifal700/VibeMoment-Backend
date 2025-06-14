using VibeMoment.Requests;
using VibeMoment.Results;

namespace VibeMoment.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> SignInAsync(SignInRequest request);
    Task SignOutAsync();
}

