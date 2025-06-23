using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class PhotoService : IPhotoService
{
    private const int EDIT_LIMIT_HOURS = 1;

    private readonly IPhotoRepository _photoRepository;

    public PhotoService(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<PhotoDto?> GetPhotoAsync(int id)
    {
        return await _photoRepository.GetByIdAsync(id);
    }

    public async Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto dto)
    {
        return await _photoRepository.SavePhotoAsync(dto);
    }

    public async Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto)
    {
        var photo = await _photoRepository.GetByIdAsync(dto.Id);
        if (photo is null)
           throw new NotFoundException("Photo not found");


        if ((DateTime.UtcNow - photo.AddedAt).TotalHours >= EDIT_LIMIT_HOURS)
        {
            throw new BusinessLogicException($"Cannot edit photo after {EDIT_LIMIT_HOURS} hours");
        }

        dto = dto with { UpdatedAt = DateTime.UtcNow };
        return await _photoRepository.UpdatePhotoAsync(dto);
    }

    public async Task DeletePhotoAsync(int id)
    {
        var photo = await _photoRepository.GetByIdAsync(id);
        if (photo is null)
            throw new NotFoundException("Photo not found");
         await _photoRepository.DeletePhotoAsync(id);
    }
}