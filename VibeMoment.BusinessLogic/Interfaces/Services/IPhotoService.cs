using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IPhotoService
{
    Task<PhotoDto> GetPhotoAsync(Guid id);
    Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto);
    Task DeletePhotoAsync(Guid id);
    Task<List<PhotoDto>> GetPhotosByUserIdAsync(PhotosQueryDto queryDto);
}