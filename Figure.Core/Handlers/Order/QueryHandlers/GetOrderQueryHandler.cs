using AutoMapper;
using Figure.Application._Queries.Order;
using Figure.Application.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order.QueryHandlers;

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, ReadOrderModel>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    public async Task<ReadOrderModel> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetAsync(o => o.Id == query.id);
        return _mapper.Map<ReadOrderModel>(order);
    }
}
