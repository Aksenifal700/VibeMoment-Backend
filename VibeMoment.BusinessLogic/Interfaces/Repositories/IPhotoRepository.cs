using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(int id);
    Task<PhotoDto> SavePhotoAsync(UploadPhotoDto uploadDto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto);
    Task<bool> DeletePhotoAsync(int id);
}