using AutoMapper;
using VibeMoment.Api.Models.Requests.Auth;
using VibeMoment.Api.Models.Responses;
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
        
        CreateMap<TokenResultDto, SignInResponse>();

    }
    
}