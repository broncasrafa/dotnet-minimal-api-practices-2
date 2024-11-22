using Microsoft.Extensions.Logging;
using Student.Application.Services.Interfaces;
using Student.Domain.Interfaces.Services;
using Student.Domain.Models;

namespace Student.Application.Services.Implementations;

internal class FileService(ILogger<FileService> logger, IStorageService storageService) : IFileService
{
    public Task<FileResult> DownloadAsync(Guid fileId)
    {
        logger.LogInformation($"Downloading image with ID: '{fileId.ToString()}'");

        return storageService.DownloadAsync(fileId);
    }

    public Task<Guid> UploadAsync(Stream stream, string filename, string contentType)
    {
        logger.LogInformation("Updating image to storage container");

        return storageService.UploadAsync(stream, filename, contentType);
    }
}
