using AutoMapper;
using Figure.Application._Commands.Figure;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Figure.CommandHandlers;
public class PostFigureCommandHandler : ICommandHandler<PostFigureCommand> {
    private readonly IFiguresRepository _figuresRepository;
    private readonly IMapper _mapper;
    private Guid createdId;

    public PostFigureCommandHandler(IFiguresRepository figuresRepository, IMapper mapper) {
        _figuresRepository = figuresRepository;
        _mapper = mapper;

    }
    public async Task Handle(PostFigureCommand command, CancellationToken cancellationToken) {
        var entity = _mapper.Map<DataAccess.Entities.Figure>(command);
        entity.CreatedAt = DateTime.Now;
        createdId = await _figuresRepository.CreateAsync(entity);
    }

    public Guid GetCreatedId() => createdId;
}

