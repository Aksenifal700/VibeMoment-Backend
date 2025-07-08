using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IPhotoService
{
    Task<PhotoDto> GetPhotoAsync(int id);
    Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto dto);
    Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto);
    Task DeletePhotoAsync(int id);
    Task<List<PhotoDto>> GetPhotosByUserIdAsync(PhotosQuery query);
}