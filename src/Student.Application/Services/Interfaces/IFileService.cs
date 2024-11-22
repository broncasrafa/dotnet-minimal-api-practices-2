using Student.Domain.Models;

namespace Student.Application.Services.Interfaces;

public interface IFileService
{
    Task<Guid> UploadAsync(Stream stream, string filename, string contentType);
    Task<FileResult> DownloadAsync(Guid fileId);
}
