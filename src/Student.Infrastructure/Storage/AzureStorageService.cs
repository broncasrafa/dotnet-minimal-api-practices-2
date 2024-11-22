using Microsoft.Extensions.Configuration;
using Student.Application.DTO.Response;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure;

namespace Student.Infrastructure.Storage;

internal class AzureStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration) : IStorageService
{
    private readonly string _ContainerName = configuration.GetSection("AzureStorageSettings:ContainerName").Value;


    public async Task<Guid> UploadAsync(Stream stream, string filename, string contentType, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(filename);
        BlobContentInfo info = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);
        return Guid.NewGuid();
    }


    public async Task<FileResponse> DownloadAsync(string filename, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(filename);
        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }
    public async Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_ContainerName);
        BlobClient blobClient = blobContainer.GetBlobClient(fileId.ToString());
        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
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
