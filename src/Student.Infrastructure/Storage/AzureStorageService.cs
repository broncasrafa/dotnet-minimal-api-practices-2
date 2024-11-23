using Microsoft.Extensions.Options;
using Student.Domain.Models;
using Student.Domain.Interfaces.Services;
using Student.Infrastructure.Settings;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Student.Infrastructure.Storage;

internal class AzureStorageService(BlobServiceClient blobServiceClient, IOptions<AzureStorageSettings> azureSettings) : IStorageService
{
    private readonly string _ContainerName = azureSettings.Value.ContainerName;


    public async Task<Guid> UploadAsync(Stream stream, string filename, string contentType, CancellationToken cancellationToken = default)
    {
        var fileId = Guid.NewGuid();
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(fileId.ToString());
        BlobContentInfo info = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);
        return fileId;
    }


    public async Task<FileResult> DownloadAsync(string filename, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(filename);
        Response<BlobDownloadResult> response = null;
        try
        {
            response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return null;
        }
        return new FileResult(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }
    public async Task<FileResult> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(fileId.ToString());
        Response<BlobDownloadResult> response = null;
        try
        {
            response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return null;
        }
        return new FileResult(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }


    public async Task DeleteAsync(string filename, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(filename);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(fileId.ToString());
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}
