namespace Figure.Core._Queries.Order;
public record GetArchivedOrdersQuery(int pageSize, int pageNumber) {
    public static GetArchivedOrdersQuery With(int pageSize, int pageNumber) {
        return new(pageSize, pageNumber);
    }
}

