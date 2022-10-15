using Figure.DataAccess.Entities;
using Figure.DataAccess.Repositories.Interfaces;

namespace Figure.DataAccess.Repositories;
public class OrdersRepository : Repository<Order>,IOrdersRepository {
    private readonly ApplicationDbContext _context;

    public OrdersRepository(ApplicationDbContext context) :base(context) {
        _context = context;
    }
    public async Task Archive(Order order) {
        order.ArchivedAt = DateTime.Now;
        order.IsArchived = true;

        await UpdateAsync(order);
    }

    public async Task DeArchive(Order order) {
        order.ArchivedAt = null;
        order.IsArchived = false;

        await UpdateAsync(order);
    }
}
