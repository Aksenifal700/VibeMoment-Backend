using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest register)
    {
        var success = await _authRepository.CreateUserAsync(register.Email, register.Password, register.Username);
        
        return new AuthResult
        {
            Success = success,
            Message = success ? "User registered successfully" : "Registration failed"
        };
    }

    public async Task<AuthResult> SignInAsync(SignInRequest signIn)
    {
        var success = await _authRepository.SignInAsync(signIn.UsernameOrEmail, signIn.Password);
        
        return new AuthResult
        {
            Success = success,
            Message = success ? "Signed in successfully" : "Invalid credentials"
        };
    }

    public async Task SignOutAsync()
    {
        await _authRepository.SignOutAsync();
    }
}