using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto);
    Task<bool> SignInAsync(SigninDto signinDto);
    Task<bool> SignOutAsync();
}

