using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        return await _authRepository.CreateUserAsync(
            dto.Email,
            dto.Password,
            dto.UserName);
    }

    public async Task<bool> SignInAsync(SigninDto dto)
    {
        return await _authRepository.SignInAsync(
            dto.UsernameOrEmail,
            dto.Password);
    }

    public async Task<bool> SignOutAsync()
    {
        await _authRepository.SignOutAsync();
        return true;
    }
}