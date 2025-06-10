using Microsoft.AspNetCore.Identity;
using VibeMoment.Requests;
using VibeMoment.Results;

namespace VibeMoment.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest register)
    {
        var user = new IdentityUser
        {
            UserName = register.Username,
            Email = register.Email
        };

        var result = await _userManager.CreateAsync(user, register.Password);
        
        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Success = false,
                Message = "Registration failed",
                Errors = result.Errors
            };
        }

        return new AuthResult
        {
            Success = true,
            Message = "User registered successfully",
            UserId = user.Id
        };
    }

    public async Task<AuthResult> SignInAsync(SignInRequest signIn)
    {
        var user = signIn.UsernameOrEmail.Contains("@")
            ? await _userManager.FindByEmailAsync(signIn.UsernameOrEmail)
            : await _userManager.FindByNameAsync(signIn.UsernameOrEmail);

        if (user is null)
        {
            return new AuthResult
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);
        
        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        return new AuthResult
        {
            Success = true,
            Message = "Signed in successfully",
            UserId = user.Id
        };
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}