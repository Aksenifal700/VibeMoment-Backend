using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.BusinessLogic.DTOs.Photodtos;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(int id);                    
    Task<PhotoDto> SavePhotoAsync(UploadPhotoDto uploadDto); 
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto); 
    Task<bool> DeletePhotoAsync(int id);
}