using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(
        Stream content,
        string blobName,
        string contentType,
        CancellationToken cancellationToken = default);
    
    Task<FileContentDto> DownloadAsync(string blobName, CancellationToken cancellationToken = default);
}