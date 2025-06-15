using VibeMoment.Api.Requests;
using VibeMoment.Api.Results;

namespace VibeMoment.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> SignInAsync(SignInRequest request);
    Task SignOutAsync();
}

