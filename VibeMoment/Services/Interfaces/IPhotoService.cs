using VibeMoment.Database.Entities;
using VibeMoment.Requests;

namespace VibeMoment.Services.Interfaces;

public interface IPhotoService
{
    Task<Photo?> GetPhotoAsync(int id);
    Task<Photo> SavePhotoAsync(SavePhotoRequest request);
    Task<Photo> UploadPhotoAsync(UploadPhotoDto dto);
    Task<Photo?> UpdatePhotoAsync(int id, UpdatePhotoRequest update);
    Task<bool> DeletePhotoAsync(int id);
}