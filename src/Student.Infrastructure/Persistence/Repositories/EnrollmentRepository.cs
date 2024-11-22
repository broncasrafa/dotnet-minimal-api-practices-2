using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories.Common;

namespace Student.Infrastructure.Persistence.Repositories;

internal class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Enrollment>> GetDetailedEnrollmentsAsync()
        => await _context.Enrollments.AsNoTracking().Include(c => c.Course).Include(s => s.Student).ToListAsync();

    public async Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(int studentId)
        => await _context.Enrollments.Where(c => c.StudentId == studentId).ToListAsync();
}
