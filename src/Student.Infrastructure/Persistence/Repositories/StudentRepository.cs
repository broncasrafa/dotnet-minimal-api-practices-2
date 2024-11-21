using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;

namespace Student.Infrastructure.Persistence.Repositories;

internal class StudentRepository(ApplicationDbContext context) : IStudentRepository
{
    public async Task<IEnumerable<Domain.Entities.Student>> GetAllAsync() 
        => await context.Students.ToListAsync();

    public async Task<Domain.Entities.Student> GetByIdAsync(int id) 
        => await context.Students.FindAsync(id);

    public async Task<Domain.Entities.Student> GetStudentDetailsAsync(int id)
        => await context.Students.Include(q => q.Enrollments).ThenInclude(q => q.Course).SingleOrDefaultAsync(c => c.Id == id);

    public async Task<Domain.Entities.Student> GetByAsync(Expression<Func<Domain.Entities.Student, bool>> predicate) 
        => await context.Students.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<Domain.Entities.Student>> FindByAsync(Expression<Func<Domain.Entities.Student, bool>> predicate) 
        => await context.Students.Where(predicate).ToListAsync();

    public async Task<Domain.Entities.Student> SingleOrDefaultAsync(Expression<Func<Domain.Entities.Student, bool>> predicate) 
        => await context.Students.SingleOrDefaultAsync(predicate);

    public async Task<Domain.Entities.Student> FirstOrDefaultAsync(Expression<Func<Domain.Entities.Student, bool>> predicate) 
        => await context.Students.FirstOrDefaultAsync(predicate);


    public async Task<Domain.Entities.Student> InsertAsync(Domain.Entities.Student entity)
    {
        await context.Set<Domain.Entities.Student>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    public async Task<Domain.Entities.Student> UpdateAsync(Domain.Entities.Student entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    public async Task DeleteAsync(Domain.Entities.Student entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}
