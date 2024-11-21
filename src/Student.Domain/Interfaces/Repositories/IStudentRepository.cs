using System.Linq.Expressions;

namespace Student.Domain.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Entities.Student>> GetAllAsync();
    Task<Entities.Student> GetByIdAsync(int id);
    Task<Entities.Student> GetStudentDetailsAsync(int id);
    Task<Entities.Student> GetByAsync(Expression<Func<Entities.Student, bool>> predicate);
    Task<IEnumerable<Entities.Student>> FindByAsync(Expression<Func<Entities.Student, bool>> predicate);
    Task<Entities.Student> SingleOrDefaultAsync(Expression<Func<Entities.Student, bool>> predicate);
    Task<Entities.Student> FirstOrDefaultAsync(Expression<Func<Entities.Student, bool>> predicate);
    Task<Entities.Student> InsertAsync(Entities.Student request);
    Task<Entities.Student> UpdateAsync(Entities.Student request);
    Task DeleteAsync(Entities.Student entity);
}
