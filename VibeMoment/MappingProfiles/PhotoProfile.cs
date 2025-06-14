using AutoMapper;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;

namespace VibeMoment.MappingProfiles;

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