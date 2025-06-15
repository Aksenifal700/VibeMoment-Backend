using VibeMoment.Api.Database.Entities;
using VibeMoment.Api.Requests;

namespace VibeMoment.Api.Services.Interfaces;

public interface IPhotoService
{
    Task<Photo?> GetPhotoAsync(int id);
    Task<Photo> UploadPhotoAsync(UploadPhotoRequest request);
    Task<bool> UpdatePhotoAsync(int id, UpdatePhotoRequest update);
    Task<bool> DeletePhotoAsync(int id);
}