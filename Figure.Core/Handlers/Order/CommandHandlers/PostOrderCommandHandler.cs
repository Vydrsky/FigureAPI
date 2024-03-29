﻿using AutoMapper;
using Figure.Application._Commands.Order;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order.CommandHandlers;
public class PostOrderCommandHandler : ICommandHandler<PostOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    private Guid createdId;

    public PostOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task Handle(PostOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DataAccess.Entities.Order>(command);
        entity.ArchivedAt = null;
        entity.CreatedAt = DateTime.Now;
        entity.IsArchived = false;
        createdId = await _ordersRepository.CreateAsync(entity);
    }

    public Guid GetCreatedId() => createdId;
}
