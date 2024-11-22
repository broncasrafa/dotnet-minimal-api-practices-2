using Student.Application.DTO.Response;

namespace Student.Infrastructure.Storage;

internal interface IStorageService
{
    Task<Guid> UploadAsync(Stream stream, string filename, string contentType, CancellationToken cancellationToken = default);

    Task<FileResponse> DownloadAsync(string filename, CancellationToken cancellationToken = default);
    Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task DeleteAsync(string filename, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
}
