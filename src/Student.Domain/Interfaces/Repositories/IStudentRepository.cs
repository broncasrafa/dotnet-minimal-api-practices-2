using System.Linq.Expressions;
using Student.Domain.Interfaces.Repositories.Common;

namespace Student.Domain.Interfaces.Repositories;

public interface IStudentRepository : IGenericRepository<Entities.Student>
{
    Task<Entities.Student> GetStudentDetailsAsync(int id);
}
