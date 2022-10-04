namespace Figure.Core.Handlers;
public interface IQueryHandler<in TQuery,TResult> {
    Task<TResult> Handle(TQuery query);
}

