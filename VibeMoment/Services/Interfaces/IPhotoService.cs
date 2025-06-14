using VibeMoment.Database.Entities;
using VibeMoment.Requests;

namespace VibeMoment.Services.Interfaces;

public interface IPhotoService
{
    Task<Photo?> GetPhotoAsync(int id);
    Task<Photo> UploadPhotoAsync(UploadPhotoRequest request);
    Task<bool> UpdatePhotoAsync(int id, UpdatePhotoRequest update);
    Task<bool> DeletePhotoAsync(int id);
}