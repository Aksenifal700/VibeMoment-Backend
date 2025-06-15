namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IAuthRepository
{
    Task<bool> CreateUserAsync(string email, string password, string username);
    Task<bool> SignInAsync(string usernameOrEmail, string password);
    Task SignOutAsync();
}