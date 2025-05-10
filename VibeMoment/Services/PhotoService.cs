using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;

namespace VibeMoment.Services;

public class PhotoService : IPhotoService
{
    public bool SavePhoto(SavePhotoRequest request)
    {
        return true;
    }
}