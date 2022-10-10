using Figure.Application._Commands.Order;
using Figure.Application.Exceptions;
using Figure.DataAccess.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order;
public class ArchiveOrderCommandHandler : ICommandHandler<ArchiveOrderCommand>{
    private readonly IOrdersRepository _ordersRepository;

    public ArchiveOrderCommandHandler(IOrdersRepository ordersRepository) {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(ArchiveOrderCommand command, CancellationToken cancellationToken) {
        var entity = await _ordersRepository.GetAsync(e => e.Id == command.Id);
        if (entity == null) {
            throw new NotFoundException("Entity not found");
        }
        await _ordersRepository.Archive(entity);
    }
}
