namespace Figure.Application._Queries.Order;
public record GetNotArchivedOrdersQuery(int pageSize, int pageNumber) {
    public static GetNotArchivedOrdersQuery With(int pageSize, int pageNumber) {
        return new(pageSize, pageNumber);
    }
}

