namespace Figure.Infrastructure;

public interface ICommandHandler<in TCommand> {
    Task Handle(TCommand command, CancellationToken cancellationToken);
}
