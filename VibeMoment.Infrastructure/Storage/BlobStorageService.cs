using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.Infrastructure.Storage;

public class BlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
    {
        string containerName =
            configuration["BlobStorage:ContainerName"]
            ?? throw new InvalidOperationException(
                "BlobStorage:ContainerName is missing.");
        
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadAsync(Stream content, string blobName, string contentType, CancellationToken cancellationToken = default)
    {
        BlobClient blobClient = _containerClient.GetBlobClient(blobName);

        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            }
        };
        
        await blobClient.UploadAsync(
            content,
            options,
            cancellationToken);

        return blobName;
    }
    
    public async Task<FileContentDto> DownloadAsync(string blobName, CancellationToken cancellationToken = default)
    {
        BlobClient blobClient = _containerClient.GetBlobClient(blobName);
        
        var response = await blobClient.DownloadStreamingAsync(
            cancellationToken: cancellationToken);

        return new FileContentDto
        {
            Content = response.Value.Content,
            ContentType = response.Value.Details.ContentType
        };
    }
}