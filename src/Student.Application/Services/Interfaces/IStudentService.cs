using System.Linq.Expressions;
using Student.Application.DTO.Request.Student;
using Student.Application.DTO.Response;
using Student.Domain.Models;

namespace Student.Application.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentResponse>> GetAllAsync();
    Task<StudentResponse> GetByIdAsync(int id);
    Task<StudentDetailsResponse> GetStudentDetailsAsync(int id);
    Task<StudentResponse> FindByAsync(Expression<Func<Domain.Entities.Student, bool>> predicate);
    Task<StudentResponse> InsertAsync(StudentCreateRequest request);
    Task<StudentResponse> UpdateAsync(StudentUpdateRequest request);
    Task DeleteAsync(int id);
    Task<FileResult> DownloadProfilePictureAsync(int studentId, Guid fileId);
    Task<string> UploadProfilePictureAsync(StudentUploadImageRequest request);
    Task UpdatePictureIdAsync(int studentId, string fileId);
    
}
