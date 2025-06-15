using AutoMapper;
using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
       
        CreateMap<SavePhotoRequest, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        
        CreateMap<UpdatePhotoRequest, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())      
            .ForMember(dest => dest.Data, opt => opt.Ignore());   
        
        CreateMap<Photo, PhotoResult>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AddedAt))
            .ForMember(dest => dest.FileName, opt => opt.Ignore()) 
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => string.Empty));
    }
}