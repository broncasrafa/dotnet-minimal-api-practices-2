using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories.Common;


namespace Student.Infrastructure.Persistence.Repositories;

internal class CourseRepository: GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Course> GetStudentListAsync(int courseId)
    {
        var courses = await _context.Courses
                                    .Include(e => e.Enrollments)
                                    .ThenInclude(s => s.Student)
                                    .FirstOrDefaultAsync(c => c.Id == courseId);
        return courses;
    }
}
