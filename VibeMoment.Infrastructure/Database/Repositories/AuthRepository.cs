using Microsoft.AspNetCore.Identity;
using VibeMoment.BusinessLogic.Services.Interfaces;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> CreateUserAsync(string email, string password, string username)
    {
        var user = new IdentityUser
        {
            UserName = username,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> SignInAsync(string usernameOrEmail, string password)
    {
        var user = usernameOrEmail.Contains("@")
            ? await _userManager.FindByEmailAsync(usernameOrEmail)
            : await _userManager.FindByNameAsync(usernameOrEmail);

        if (user is null) return false;

        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, true);
        return result.Succeeded;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}