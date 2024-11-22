using Student.Domain.Models;

namespace Student.Domain.Interfaces.Services;

public interface IStorageService
{
    Task<Guid> UploadAsync(Stream stream, string filename, string contentType, CancellationToken cancellationToken = default);

    Task<FileResult> DownloadAsync(string filename, CancellationToken cancellationToken = default);
    Task<FileResult> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task DeleteAsync(string filename, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
}
