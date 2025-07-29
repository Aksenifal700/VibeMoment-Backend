using AutoMapper;
using VibeMoment.Api.Models.Requests.Auth;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.MappingProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, RegisterDto>();
        CreateMap<RegisterDto, User>();
        
        CreateMap<SignInRequest, SigninDto>();

        CreateMap<CreateRefreshTokenDto, RefreshToken>();
        CreateMap<RefreshToken, RefreshTokenDto>();
        
        CreateMap<SignInRequest, SigninDto>();
    }
    
}