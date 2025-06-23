using AutoMapper;
using VibeMoment.Api.Models.Requests.Auth;
using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.Api.MappingProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, RegisterDto>();
        CreateMap<SignInRequest, SigninDto>();

    }
    
}