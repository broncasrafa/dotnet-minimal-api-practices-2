using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories.Common;

namespace Student.Domain.Interfaces.Repositories;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(int studentId);
}
