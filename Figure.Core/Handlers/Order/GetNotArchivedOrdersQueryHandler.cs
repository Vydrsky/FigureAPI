using AutoMapper;
using Figure.Core._Queries.Order;
using Figure.Core.Models.Order;
using Figure.DataAccess.Interfaces;
using Figure.Infrastructure;

namespace Figure.Core.Handlers.Order;

public class GetNotArchivedOrdersQueryHandler : IQueryHandler<GetNotArchivedOrdersQuery, IEnumerable<ReadOrderModel>> {
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    public GetNotArchivedOrdersQueryHandler(IOrdersRepository ordersRepository,IMapper mapper) {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ReadOrderModel>> Handle(GetNotArchivedOrdersQuery query, CancellationToken cancellationToken) {
        var orders = await _ordersRepository.GetAllAsync(query.pageSize,query.pageNumber, o => o.IsArchived == false);
        return _mapper.Map<List<ReadOrderModel>>(orders);
    }
}

