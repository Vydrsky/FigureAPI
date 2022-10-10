
using Figure.DataAccess.Entities;

namespace Figure.DataAccess.Interfaces;
public interface IOrdersRepository : IRepository<Order>{
    Task Archive(Order order);
    Task DeArchive(Order order);
}
