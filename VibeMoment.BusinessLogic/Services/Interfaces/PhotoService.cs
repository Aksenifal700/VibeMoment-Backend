using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;

namespace VibeMoment.BusinessLogic.Services.Interfaces;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    
    public PhotoService(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    public async Task<PhotoResult?> GetPhotoAsync(int id)
    {
        return await _photoRepository.GetByIdAsync(id);
    }

    public async Task<PhotoResult> UploadPhotoAsync(UploadPhotoRequest request)
    {
        return await _photoRepository.SavePhotoAsync(request);
    }

    public async Task<bool> UpdatePhotoAsync(int id, UpdatePhotoRequest request)
    {
        var result = await _photoRepository.UpdatePhotoAsync(id, request);
        return result.Success;
    }

    public async Task<bool> DeletePhotoAsync(int id)
    {
        return await _photoRepository.DeletePhotoAsync(id);
    }
}