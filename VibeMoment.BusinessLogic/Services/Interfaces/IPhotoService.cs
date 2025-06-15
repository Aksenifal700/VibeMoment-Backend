using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IPhotoService
{
    Task<PhotoResult?> GetPhotoAsync(int id);
    Task<PhotoResult> UploadPhotoAsync(UploadPhotoRequest request);
    Task<bool> UpdatePhotoAsync(int id, UpdatePhotoRequest update);
    Task<bool> DeletePhotoAsync(int id);
}