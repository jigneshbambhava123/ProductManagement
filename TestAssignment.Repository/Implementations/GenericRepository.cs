using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TestAssignment.Entity.Data;
using TestAssignment.Repository.Interfaces;

namespace TestAssignment.Repository.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TestAssignmentContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TestAssignmentContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(entity => EF.Property<bool>(entity, "Isdeleted") == false).ToListAsync();
    }


    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if(entity !=null){
            var property = entity.GetType().GetProperty("Isdeleted");
            if(property != null){
                property.SetValue(entity, true);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}
