using AutoMapper;
using VibeMoment.Api.Requests;
using VibeMoment.Api.Responses;
using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.Api.MappingProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, RegisterUserDto>();
        CreateMap<SignInRequest, SigninUserDto>();
        
        CreateMap<AuthResultDto, AuthResponse>();
    }
    
}