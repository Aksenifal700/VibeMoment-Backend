using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class PhotoService : IPhotoService
{
    private const int EDIT_LIMIT_HOURS = 1;

    private readonly IPhotoRepository _photoRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<PhotoService> _logger;

    public PhotoService(IPhotoRepository photoRepository, IFileStorageService fileStorageService, ILogger<PhotoService> logger)
    {
        _photoRepository = photoRepository;
        _fileStorageService = fileStorageService;
        _logger = logger;
    }
    
    public async Task<PhotoDto> GetPhotoAsync(Guid id)
    {
        var photo = await _photoRepository.GetByIdAsync(id);
        if (photo is null)
            throw new NotFoundException("Photo not found"); 
        
        return photo;
    }
    
    public async Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto dto, Stream content, string contentType)
    {
        string blobName = $"photos-{Guid.NewGuid()}";
        
        dto.BlobName = await _fileStorageService.UploadAsync(
            content,
            blobName,
            contentType);
        
        dto.Data = [];
        
        try
        {
            await _photoRepository.SaveAsync(dto);
        }
        catch 
        {
            try
            {
                await _fileStorageService.DeleteAsync(blobName);
            }
            catch(Exception cleanupException)
            {
                _logger.LogError(cleanupException, "Failed to clean up blob {BlobName} after saving photo failed",
                    blobName);
            }

            throw;
        }
        
        return await _photoRepository.SaveAsync(dto);
    }

    public async Task<FileContentDto> GetPhotoContentAsync(Guid photoId)
    {
       var photo = await _photoRepository.GetByIdAsync(photoId);
       if(photo is null)
           throw new NotFoundException("Photo not found");

       if (!string.IsNullOrWhiteSpace(photo.BlobName))
       {
           return await _fileStorageService.DownloadAsync(photo.BlobName);
       }
       
       var data = photo.Data;
       
       if (photo.Data is null || photo.Data.Length == 0)
           throw new NotFoundException("Photo context not found");
       
       string contentType;

       if (data.Length >= 3 &&
           data[0] == 0xFF &&
           data[1] == 0xD8 &&
           data[2] == 0xFF)
       {
           contentType = "image/jpeg";
       }
       else if (data.Length >= 8 &&
                data[0] == 0x89 &&
                data[1] == 0x50 &&
                data[2] == 0x4E &&
                data[3] == 0x47 &&
                data[4] == 0x0D &&
                data[5] == 0x0A &&
                data[6] == 0x1A &&
                data[7] == 0x0A)
       {
           contentType = "image/png";
       }
       else
       {
           throw new BusinessLogicException(
               "Unsupported image format.");
       }

       return new FileContentDto
       {
           Content = new MemoryStream(data, writable: false),
           ContentType = contentType
       };
       
    }

    public async Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto, Guid currentUserId)
    {
        var photo = await _photoRepository.GetByIdAsync(dto.Id);
        if (photo is null)
           throw new NotFoundException("Photo not found");
        
        if (photo.UserId != currentUserId)
            throw new ForbiddenAccessException("You do not have permission to access this resource.");


        if ((DateTime.UtcNow - photo.AddedAt).TotalHours >= EDIT_LIMIT_HOURS)
        {
            throw new BusinessLogicException($"Cannot edit photo after {EDIT_LIMIT_HOURS} hours");
        }

        dto.UpdatedAt = DateTime.UtcNow;
        return await _photoRepository.UpdateAsync(dto);
    }

    public async Task DeletePhotoAsync(Guid id, Guid currentUserId)
    {
        var photo = await _photoRepository.GetByIdAsync(id);
        if (photo is null)
            throw new NotFoundException("Photo not found"); 
        
        if(photo.UserId != currentUserId)
            throw new ForbiddenAccessException("You do not have permission to access this resource.");
        
        if (!string.IsNullOrWhiteSpace(photo.BlobName))
        { 
            await _fileStorageService.DeleteAsync(photo.BlobName);
        }
        
        await _photoRepository.DeleteAsync(id);
    }

    public async Task<PageResult<PhotoDto>> GetPhotosByUserIdAsync(PhotosQueryDto queryDto)
    {
      return await _photoRepository.GetByUserIdAsync(queryDto);
    }
}