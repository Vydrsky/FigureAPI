using Figure.DataAccess.Entities;
using Figure.DataAccess.Interfaces;

namespace Figure.DataAccess.Repositories;
public class OrdersRepository : Repository<Order>,IOrdersRepository {
    private readonly ApplicationDbContext _context;

    public OrdersRepository(ApplicationDbContext context) :base(context) {
        _context = context;
    }
    public async Task Archive(Order order) {
        Order entity = _context.Orders.Where(o => o.Id == order.Id).FirstOrDefault();

        if (entity == null)
            return;

        entity.ArchivedAt = DateTime.Now;
        entity.IsArchived = true;

        await UpdateAsync(entity);
    }

    public async Task UnArchive(Order order) {
        Order entity = _context.Orders.Where(o => o.Id == order.Id).FirstOrDefault();

        if (entity == null)
            return;

        entity.ArchivedAt = null;
        entity.IsArchived = false;

        await UpdateAsync(entity);
    }
}
