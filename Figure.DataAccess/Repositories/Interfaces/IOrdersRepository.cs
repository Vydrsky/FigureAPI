using Figure.DataAccess.Entities;
using Figure.Infrastructure;

namespace Figure.DataAccess.Repositories.Interfaces;
public interface IOrdersRepository : IRepository<Order>
{
    Task Archive(Order order);
    Task DeArchive(Order order);
}
