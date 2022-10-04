using Figure.DataAccess.Entities;
using Figure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Figure.DataAccess.Repositories;
public class Repository<T> : IRepository<T> where T : class,IEntity,new() {
    private readonly ApplicationDbContext _context;
    private DbSet<T> _set;

    public Repository(ApplicationDbContext context) {
        _context = context;
        _set = _context.Set<T>();
    }
    public async Task CreateAsync(T entity) {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(int pageSize, int pageNumber, Expression<Func<T, bool>>? filter = null) {
        IQueryable<T> query = _set;

        if (query == null) {
            return new List<T>();
        }

        if (filter != null) {
            query = query.Where(filter);
        }

        if (pageSize > 0) {
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true) {
        IQueryable<T> query = _set;
        if(query == null) {
            return new T();
        }

        if(filter != null) {
            query = query.Where(filter);
        }

        if (tracked) {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(T entity) {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity) {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}
