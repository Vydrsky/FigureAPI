namespace Figure.Core.Handlers;

public interface ICommandHandler<in TCommand> {
    Task Handle(TCommand command);
}
