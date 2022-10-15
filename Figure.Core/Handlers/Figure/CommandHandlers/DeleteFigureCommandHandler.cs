using AutoMapper;
using Figure.Application._Commands.Figure;
using Figure.Application.Exceptions;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;

namespace Figure.Application.Handlers.Figure.CommandHandlers;

public class DeleteFigureCommandHandler : ICommandHandler<DeleteFigureCommand> {
    private readonly IFiguresRepository _figuresRepository;

    public DeleteFigureCommandHandler(IFiguresRepository figuresRepository, IMapper mapper) {
        _figuresRepository = figuresRepository;
    }

    public async Task Handle(DeleteFigureCommand command, CancellationToken cancellationToken) {
        var entity = await _figuresRepository.GetAsync(e => e.Id == command.Id);
        if(entity == null) {
            throw new NotFoundException("Entity not found");
        }
        await _figuresRepository.RemoveAsync(entity);
    }
}

