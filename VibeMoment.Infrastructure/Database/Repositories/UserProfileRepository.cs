using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserProfileRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserProfileDto?> GetByUserIdAsync(Guid userId)
    {
        var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            return profile is null
                ? null
                : _mapper.Map<UserProfileDto>(profile);
    }

    public async Task<UserProfileDto?> UpdateUserProfileAsync(Guid userId, UpdateUserProfileDto dto)
    {
        var existingUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (existingUserProfile is null)
                throw new NotFoundException("User profile not found");

            _mapper.Map(dto, existingUserProfile);

            await _context.SaveChangesAsync();
            
            return _mapper.Map<UserProfileDto>(existingUserProfile);
    }

    public async Task<UserProfileDto?> UploadUserAvatarAsync(Guid userId, byte[] avatar)
    {
        var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if(profile is null)
               throw new NotFoundException("User profile not found");
            profile.Avatar = avatar;
            
            await _context.SaveChangesAsync();

            return _mapper.Map<UserProfileDto>(profile);
    }
    
}