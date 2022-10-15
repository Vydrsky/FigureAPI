using AutoMapper;
using Figure.Application._Queries.Figure;
using Figure.Application.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Figure.QueryHandlers;

public class GetAllFiguresQueryHandler : IQueryHandler<GetAllFiguresQuery,IEnumerable<ReadFigureModel>> {

    private readonly IFiguresRepository _figuresRepository;
    private readonly IMapper _mapper;
    
    public GetAllFiguresQueryHandler(IFiguresRepository figuresRepository, IMapper mapper) {
        _figuresRepository = figuresRepository;
        _mapper = mapper;
    
    }
    public async Task<IEnumerable<ReadFigureModel>> Handle(GetAllFiguresQuery query, CancellationToken cancellationToken) {
        var orders = await _figuresRepository.GetAllAsync(query.pageSize, query.pageNumber);
        return _mapper.Map<List<ReadFigureModel>>(orders);
    }
}
