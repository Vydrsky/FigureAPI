namespace Figure.Core._Queries.Order;
public record GetAllOrdersQuery(int pageSize, int pageNumber) {
    public static GetAllOrdersQuery With(int pageSize, int pageNumber) {
        return new(pageSize, pageNumber);
    }
}

