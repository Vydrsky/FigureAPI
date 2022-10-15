namespace Figure.Application._Queries.Figure;

public record GetFigureQuery(Guid id)
{
    public static GetFigureQuery With(Guid id)
    {
        return new GetFigureQuery(id);
    }
}
