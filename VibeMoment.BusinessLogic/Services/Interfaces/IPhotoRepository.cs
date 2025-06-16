using VibeMoment.BusinessLogic.DTOs;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(int id);                    
    Task<PhotoDto> SavePhotoAsync(UploadPhotoDto uploadDto); 
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto); 
    Task<bool> DeletePhotoAsync(int id);
}