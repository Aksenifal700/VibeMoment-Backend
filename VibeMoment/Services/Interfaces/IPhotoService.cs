using VibeMoment.Requests;

namespace VibeMoment.Services.Interfaces;

public interface IPhotoService
{
    public bool SavePhoto(SavePhotoRequest request);
}