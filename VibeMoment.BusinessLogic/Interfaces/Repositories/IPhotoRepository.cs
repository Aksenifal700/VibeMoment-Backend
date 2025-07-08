using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(int id);
    Task<PhotoDto> SaveAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdateAsync(UpdatePhotoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<PhotoDto>>GetByUserIdAsync(PhotosQuery query);
}