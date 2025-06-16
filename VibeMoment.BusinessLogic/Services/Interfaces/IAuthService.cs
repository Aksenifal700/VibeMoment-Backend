using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterUserDto registerDto);
    Task<AuthResultDto> SignInAsync(SigninUserDto signinDto);
    Task<bool> SignOutAsync();
}

