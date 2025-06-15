using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IPhotoRepository
{
    Task<PhotoResult> GetByIdAsync(int id);
    Task<PhotoResult> SavePhotoAsync(UploadPhotoRequest request);
    Task<PhotoResult> UpdatePhotoAsync(int id, UpdatePhotoRequest request);
    Task<bool> DeletePhotoAsync(int id);
}