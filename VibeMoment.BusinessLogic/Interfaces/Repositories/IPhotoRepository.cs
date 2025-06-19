using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(int id);
    Task<PhotoDto> SavePhotoAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto);
    Task<bool> DeletePhotoAsync(int id);
}