using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IPhotoService
{
    Task<PhotoDto> GetPhotoAsync(Guid id);
    Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto, Guid userId);
    Task DeletePhotoAsync(Guid id);
    Task<PageResult<PhotoDto>> GetPhotosByUserIdAsync(PhotosQueryDto queryDto);
}