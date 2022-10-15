namespace Figure.Application._Queries.Figure;

public record GetAllFiguresQuery(int pageSize, int pageNumber) {
    public static GetAllFiguresQuery With(int pageSize, int pageNumber) {
        return new(pageSize, pageNumber);
    }
}
