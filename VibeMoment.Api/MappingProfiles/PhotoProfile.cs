using AutoMapper;
using VibeMoment.Api.Requests.Photo;
using VibeMoment.Api.Responses;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
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