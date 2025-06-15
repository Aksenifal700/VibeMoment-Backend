using AutoMapper;
using VibeMoment.Api.Database.Entities;
using VibeMoment.Api.Requests;

namespace VibeMoment.Api.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
       
        CreateMap<SavePhotoRequest, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        
        CreateMap<UpdatePhotoRequest, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())      
            .ForMember(dest => dest.Data, opt => opt.Ignore());   
    }
}