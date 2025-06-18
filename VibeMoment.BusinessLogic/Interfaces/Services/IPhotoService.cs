using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IPhotoService
{
    Task<PhotoDto?> GetPhotoAsync(int id);
    Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto uploadDto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto);
    Task DeletePhotoAsync(int id);
}