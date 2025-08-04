using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<bool> CreateUserAsync(RegisterDto dto);
    Task<Guid?> GetValidUserIdAsync(string usernameOrEmail, string password);
    Task<UserDto> GetByIdAsync(Guid userId);
}