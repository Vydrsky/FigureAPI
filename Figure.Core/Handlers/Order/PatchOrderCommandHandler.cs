using Figure.Application._Commands;
using Figure.Application.Exceptions;
using Figure.DataAccess.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order;
public class PatchOrderCommandHandler : ICommandHandler<PatchOrderCommand> {
    private readonly IOrdersRepository _ordersRepository;

    public PatchOrderCommandHandler(IOrdersRepository ordersRepository) {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(PatchOrderCommand command, CancellationToken cancellationToken) {
        var entity = await _ordersRepository.GetAsync(e => e.Id == command.Id);
        if (entity == null) {
            throw new NotFoundException("Entity not found");
        }
        command.JsonPatchDocument.ApplyTo(entity);
        await _ordersRepository.UpdateAsync(entity);
    }
}
