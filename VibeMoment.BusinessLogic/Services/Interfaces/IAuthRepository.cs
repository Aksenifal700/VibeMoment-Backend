
namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IAuthRepository
{
    Task<bool> CreateUserAsync(string email, string password, string userName);
    Task<bool> SignInAsync(string usernameOrEmail, string password);
    Task SignOutAsync();
    
}
    