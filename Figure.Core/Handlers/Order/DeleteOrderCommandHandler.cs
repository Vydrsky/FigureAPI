using Figure.Application._Commands;
using Figure.Application.Exceptions;
using Figure.DataAccess.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order;
public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand> {
    private readonly IOrdersRepository _ordersRepository;

    public DeleteOrderCommandHandler(IOrdersRepository ordersRepository) {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken) {
        var entity = await _ordersRepository.GetAsync(e => e.Id == command.Id);
        if(entity == null) {
            throw new NotFoundException("Entity not found");
        }
        await _ordersRepository.RemoveAsync(entity);
    }
}
