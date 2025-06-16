using AutoMapper;
using VibeMoment.Api.Requests;
using VibeMoment.Api.Responses;
using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {

        CreateMap<UploadPhotoRequest, UploadPhotoDto>()
            .ForMember(dest => dest.PhotoData, opt => opt.Ignore())
            .ForMember(dest => dest.FileName, opt => opt.Ignore());
            
        CreateMap<UpdatePhotoRequest, UpdatePhotoDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<UploadPhotoDto, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.PhotoData))
            .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        
        CreateMap<Photo, PhotoDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        
        CreateMap<PhotoDto, PhotoResponse>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AddedAt));
        
    }
}