using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetProfileAsync(Guid userId);
    Task<UserProfileDto?> UpdateProfileAsync(UpdateUserProfileDto dto, Guid userId);
    Task<UserProfileDto?> UploadAvatarAsync(Guid userId, byte[] avatar);
}