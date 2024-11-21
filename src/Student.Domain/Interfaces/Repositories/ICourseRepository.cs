using System.Linq.Expressions;
using Student.Domain.Entities;

namespace Student.Domain.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int id);
    Task<Course> GetByAsync(Expression<Func<Course, bool>> predicate);
    Task<IEnumerable<Course>> FindByAsync(Expression<Func<Course, bool>> predicate);
    Task<Course> SingleOrDefaultAsync(Expression<Func<Course, bool>> predicate);
    Task<Course> FirstOrDefaultAsync(Expression<Func<Course, bool>> predicate);
    Task<Course> InsertAsync(Course request);
    Task<Course> UpdateAsync(Course request);
    Task DeleteAsync(Course entity);
}
