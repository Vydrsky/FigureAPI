using AutoMapper;
using Figure.Application._Queries.Order;
using Figure.Application.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Order.QueryHandlers;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<ReadOrderModel>>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    public GetAllOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ReadOrderModel>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllAsync(query.pageSize, query.pageNumber);
        return _mapper.Map<List<ReadOrderModel>>(orders);
    }
}

