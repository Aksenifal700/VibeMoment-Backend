using AutoMapper;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;

namespace VibeMoment.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
        // Налаштування мапінгу SavePhotoRequest -> Photo
        CreateMap<SavePhotoRequest, Photo>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Наприклад, пропускаємо Id
    }
}