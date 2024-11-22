using Microsoft.Extensions.Logging;
using Student.Domain.Interfaces.Services;
using Student.Infrastructure.Storage;

namespace Student.Infrastructure.Services;

internal class FileUploader(ILogger<FileUploader> logger, IStorageService storageService) : IFileUploader
{

}
