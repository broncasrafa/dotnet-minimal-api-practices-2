using Microsoft.EntityFrameworkCore;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories.Common;

namespace Student.Infrastructure.Persistence.Repositories;

internal class StudentRepository : GenericRepository<Domain.Entities.Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Student> GetStudentDetailsAsync(int id)
        => await _context.Students.Include(q => q.Enrollments).ThenInclude(q => q.Course).SingleOrDefaultAsync(c => c.Id == id);
}
