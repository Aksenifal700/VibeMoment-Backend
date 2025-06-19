using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);
    Task<bool> SignInAsync(SigninDto dto);
    Task<bool> SignOutAsync();
}