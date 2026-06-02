using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfileDto?> GetByUserIdAsync(Guid userId);
    Task<UserProfileDto?> UpdateUserProfileAsync(Guid userId,UpdateUserProfileDto dto);
    Task<UserProfileDto?> UploadUserAvatarAsync(Guid userId, byte[] avatar);
}