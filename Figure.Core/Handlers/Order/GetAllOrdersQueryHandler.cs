using AutoMapper;
using Figure.Core.Models.Order;
using Figure.Core.Queries;
using Figure.DataAccess.Interfaces;

namespace Figure.Core.Handlers.Order;
public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<ReadOrderModel>> {
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    public GetAllOrdersQueryHandler(IOrdersRepository ordersRepository,IMapper mapper) {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ReadOrderModel>> Handle(GetAllOrdersQuery query) {
        var orders = await _ordersRepository.GetAllAsync(query.pageSize,query.pageNumber);
        return _mapper.Map<List<ReadOrderModel>>(orders);
    }
}

