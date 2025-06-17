using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.BusinessLogic.Services.Interfaces;

namespace VibeMoment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        return await _authRepository.CreateUserAsync(
            registerDto.Email, 
            registerDto.Password, 
            registerDto.UserName);
    }

    public async Task<bool> SignInAsync(SigninDto signinDto)
    {
        return await _authRepository.SignInAsync(
            signinDto.UsernameOrEmail , 
            signinDto.Password);
    }

    public async Task<bool> SignOutAsync()
    {
        await _authRepository.SignOutAsync();
        return true;
    }
}
