using AutoMapper;
using Figure.Core._Queries.Order;
using Figure.Core.Models.Order;
using Figure.DataAccess.Interfaces;
using Figure.Infrastructure;

namespace Figure.Core.Handlers.Order;

public class GetArchivedOrdersQueryHandler : IQueryHandler<GetArchivedOrdersQuery, IEnumerable<ReadOrderModel>> {
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    public GetArchivedOrdersQueryHandler(IOrdersRepository ordersRepository,IMapper mapper) {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ReadOrderModel>> Handle(GetArchivedOrdersQuery query, CancellationToken cancellationToken) {
        var orders = await _ordersRepository.GetAllAsync(query.pageSize,query.pageNumber, o => o.IsArchived == true);
        return _mapper.Map<List<ReadOrderModel>>(orders);
    }
}

