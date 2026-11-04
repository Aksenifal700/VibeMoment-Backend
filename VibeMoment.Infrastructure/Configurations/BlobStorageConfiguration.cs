using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeMoment.BusinessLogic.Interfaces.Services;
using VibeMoment.Infrastructure.Storage;
using Azure.Identity;

namespace VibeMoment.Infrastructure.Configurations;

public static class BlobStorageConfiguration
{
    public static void AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.UseCredential(new AzureCliCredential());
            
            clientBuilder.AddBlobServiceClient(
                new Uri(configuration["BlobStorage:ServiceUri"]
                ?? throw new InvalidOperationException(
                    "BlobStorage:ServiceUri is missing.")));
            
        });
        services.AddScoped<IFileStorageService, BlobStorageService>();
    }
}