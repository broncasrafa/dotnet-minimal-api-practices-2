using System.Linq.Expressions;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Domain.Entities;

namespace Student.Application.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseResponse>> GetAllAsync();
    Task<CourseResponse> GetByIdAsync(int id);
    Task<CourseResponse> FindByAsync(Expression<Func<Course, bool>> predicate);
    Task<CourseResponse> InsertAsync(CourseCreateRequest request);
    Task<CourseResponse> UpdateAsync(CourseUpdateRequest request);
    Task DeleteAsync(int id);
}
