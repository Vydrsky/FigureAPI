using System.Linq.Expressions;

namespace Figure.Infrastructure;
public interface IRepository<T> where T : IEntity {
    Task<Guid> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>>? filter, bool tracked = true);
    Task<IEnumerable<T>> GetAllAsync(int pageSize, int pageNumber, Expression<Func<T, bool>>? filter = null);
}

