namespace Figure.Core._Queries.Order;

public record GetOrderQuery(Guid id) {
    public static GetOrderQuery With(Guid id) {
        return new GetOrderQuery(id);
    }
}
