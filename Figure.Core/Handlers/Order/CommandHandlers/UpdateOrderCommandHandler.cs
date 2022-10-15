using AutoMapper;
using Figure.Application._Commands.Order;
using Figure.Application.Exceptions;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order.CommandHandlers;
public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DataAccess.Entities.Order>(command);
        if (await _ordersRepository.GetAsync(e => e.Id == entity.Id) == null)
        {
            throw new NotFoundException("Entity not found");
        }
        await _ordersRepository.UpdateAsync(entity);
    }
}

