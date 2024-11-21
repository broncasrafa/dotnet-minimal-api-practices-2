using System.Linq.Expressions;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories.Common;

namespace Student.Domain.Interfaces.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course> GetStudentListAsync(int courseId);
}
