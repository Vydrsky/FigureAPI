using Figure.DataAccess.Entities;

namespace Figure.DataAccess.Repositories.Interfaces;
public interface IOrdersRepository : IRepository<Order>
{
    Task Archive(Order order);
    Task DeArchive(Order order);
}
