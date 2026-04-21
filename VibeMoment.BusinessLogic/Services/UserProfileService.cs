using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileService(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
    {
        var profile = await _userProfileRepository.GetByUserIdAsync(userId);
        if (profile is null)
            throw new NotFoundException("User not found");

        return profile;
    }

    public async Task<UserProfileDto?> UpdateProfileAsync(UpdateUserProfileDto dto, Guid userId)
    {
        return await _userProfileRepository.UpdateUserProfileAsync(userId, dto);
    }

    public async Task<UserProfileDto?> UploadAvatarAsync(Guid userId, byte[] avatar)
    {
        return await _userProfileRepository.UploadUserAvatarAsync(userId, avatar);
    }
}