using AutoMapper;
using Figure.Application._Queries.Figure;
using Figure.Application.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Figure.QueryHandlers;

public class GetFigureQueryHandler : IQueryHandler<GetFigureQuery,ReadFigureModel> {

    private readonly IFiguresRepository _figuresRepository;
    private readonly IMapper _mapper;
    
    public GetFigureQueryHandler(IFiguresRepository figuresRepository, IMapper mapper) {
        _figuresRepository = figuresRepository;
        _mapper = mapper;
    
    }
    public async Task<ReadFigureModel> Handle(GetFigureQuery query, CancellationToken cancellationToken) {
        var orders = await _figuresRepository.GetAsync(e => e.Id == query.id);
        return _mapper.Map<ReadFigureModel>(orders);
    }
}
