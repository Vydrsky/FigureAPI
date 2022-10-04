using Figure.Core.Entities;
using System.Linq.Expressions;

namespace Figure.DataAccess.Interfaces;
internal interface IRepository<T> where T : IEntity {
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<T> GetAsync(Expression<Func<T,bool>>? filter,bool tracked = true);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, int pageSize, int pageNumber);
}

