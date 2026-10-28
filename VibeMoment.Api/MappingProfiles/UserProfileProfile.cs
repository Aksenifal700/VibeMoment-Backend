using AutoMapper;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.MappingProfiles;

public class UserProfileProfile : Profile
{
    public UserProfileProfile()
    {
        CreateMap<UserProfile, UserProfileDto>();
        
        CreateMap<UpdateUserProfileDto, UserProfile>();
    }
}