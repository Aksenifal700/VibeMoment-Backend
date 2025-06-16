using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterUserDto registerDto)
    {
        var success = await _authRepository.CreateUserAsync(
            registerDto.Email, 
            registerDto.Password, 
            registerDto.UserName);
            
        return new AuthResultDto
        {
            Success = success,
            Message = success ? "User registered successfully" : "Registration failed"
        };
    }

    public async Task<AuthResultDto> SignInAsync(SigninUserDto signinDto)
    {
        var success = await _authRepository.SignInAsync(signinDto.UsernameOrEmail , signinDto.Password);
            
        return new AuthResultDto
        {
            Success = success,
            Message = success ? "Signed in successfully" : "Invalid credentials"
        };
    }

    public async Task<bool> SignOutAsync()
    {
        await _authRepository.SignOutAsync();
        return true;
    }
}
