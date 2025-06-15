using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> SignInAsync(SignInRequest request);
    Task SignOutAsync();
}

