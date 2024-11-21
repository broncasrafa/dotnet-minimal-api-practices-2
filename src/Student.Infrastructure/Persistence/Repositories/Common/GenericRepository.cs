using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities.Common;
using Student.Domain.Interfaces.Repositories.Common;
using Student.Infrastructure.Persistence.Context;

namespace Student.Infrastructure.Persistence.Repositories.Common;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<TEntity>> GetAllAsync() 
        => await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetByIdAsync(int id) 
        => await _context.Set<TEntity>().FindAsync(id);

    public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _context.Set<TEntity>().Where(predicate).ToListAsync();

    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) 
        => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);


    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> ExistsAsync(int id)
        => await _context.Set<TEntity>().AnyAsync(q => q.Id == id);
}
