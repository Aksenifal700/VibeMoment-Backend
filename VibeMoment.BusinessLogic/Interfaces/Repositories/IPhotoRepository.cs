using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task<PhotoDto?> GetByIdAsync(Guid id);
    Task<PhotoDto> SaveAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdateAsync(UpdatePhotoDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<PageResult<PhotoDto>>GetByUserIdAsync(PhotosQueryDto queryDto);
}