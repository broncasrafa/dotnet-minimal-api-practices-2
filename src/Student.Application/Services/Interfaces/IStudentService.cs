using System.Linq.Expressions;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;

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
}
