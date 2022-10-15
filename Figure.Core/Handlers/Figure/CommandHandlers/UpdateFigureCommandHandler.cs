using AutoMapper;
using Figure.Application._Commands.Figure;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Figure.CommandHandlers;
public class UpdateFigureCommandHandler : ICommandHandler<UpdateFigureCommand> {
    private readonly IFiguresRepository _figuresRepository;
    private readonly IMapper _mapper;

    public UpdateFigureCommandHandler(IFiguresRepository figuresRepository, IMapper mapper) {
        _figuresRepository = figuresRepository;
        _mapper = mapper;

    }
    public async Task Handle(UpdateFigureCommand command, CancellationToken cancellationToken) {
        var entity = _mapper.Map<DataAccess.Entities.Figure>(command);
        await _figuresRepository.UpdateAsync(entity);
    }
}

