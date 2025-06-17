using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.BusinessLogic.DTOs.Authdtos;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto);
    Task<bool> SignInAsync(SigninDto signinDto);
    Task<bool> SignOutAsync();
}

