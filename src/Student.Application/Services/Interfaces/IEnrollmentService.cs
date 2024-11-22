using System.Linq.Expressions;
using Student.Application.DTO.Request.Enrollment;
using Student.Application.DTO.Response;
using Student.Domain.Entities;

namespace Student.Application.Services.Interfaces;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentResponse>> GetStudentEnrollmentsAsync(int studentId);
    Task<IEnumerable<EnrollmentResponse>> GetAllAsync();
    Task<EnrollmentResponse> GetByIdAsync(int id);
    Task<EnrollmentResponse> FindByAsync(Expression<Func<Enrollment, bool>> predicate);
    Task<EnrollmentResponse> InsertAsync(EnrollmentCreateRequest request);
    Task DeleteAsync(int id);
}
