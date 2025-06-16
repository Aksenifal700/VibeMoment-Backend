using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IPhotoService
{
    Task<PhotoDto?> GetPhotoAsync(int id);
    Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto uploadDto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto);
    Task<bool> DeletePhotoAsync(int id);
   
}