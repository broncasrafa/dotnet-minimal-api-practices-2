using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;


namespace Student.Infrastructure.Persistence.Repositories;

internal class CourseRepository(ApplicationDbContext context) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAllAsync() => await context.Courses.ToListAsync();
    public async Task<Course> GetByIdAsync(int id) => await context.Courses.FindAsync(id);
    public async Task<Course> GetByAsync(Expression<Func<Course, bool>> predicate) => await context.Courses.FirstOrDefaultAsync(predicate);
    public async Task<IEnumerable<Course>> FindByAsync(Expression<Func<Course, bool>> predicate) => await context.Courses.Where(predicate).ToListAsync();
    public async Task<Course> SingleOrDefaultAsync(Expression<Func<Course, bool>> predicate) => await context.Courses.SingleOrDefaultAsync(predicate);
    public async Task<Course> FirstOrDefaultAsync(Expression<Func<Course, bool>> predicate) => await context.Courses.FirstOrDefaultAsync(predicate);


    public async Task<Course> InsertAsync(Course entity)
    {
        await context.Set<Course>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    public async Task<Course> UpdateAsync(Course entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    public async Task DeleteAsync(Course entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}
