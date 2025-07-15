namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<bool> CreateUserAsync(string email, string password, string userName);
    Task SignOutAsync();
    Task<Guid?> GetValidUserIdAsync(string usernameOrEmail, string password);
}