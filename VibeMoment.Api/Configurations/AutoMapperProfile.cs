using AutoMapper;
using VibeMoment.Api.Requests.Auth;
using VibeMoment.Api.Requests.Photo;
using VibeMoment.Api.Responses;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.Configurations;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapper for Auth
        CreateMap<RegisterRequest, RegisterDto>();
        CreateMap<SignInRequest, SigninDto>();
        
        //Mapper for Photo
        CreateMap<UploadPhotoRequest, UploadPhotoDto>();

        CreateMap<UpdatePhotoRequest, UpdatePhotoDto>();

        CreateMap<UpdatePhotoDto, Photo>();

        CreateMap<UploadPhotoDto, Photo>();
        CreateMap<Photo, PhotoDto>();

        CreateMap<PhotoDto, PhotoResponse>()
            .ForMember(dest => dest.ImageData, 
                opt => opt.MapFrom(src => Convert.ToBase64String(src.Data)));
    }
}