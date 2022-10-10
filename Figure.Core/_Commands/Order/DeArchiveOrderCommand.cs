namespace Figure.Application._Commands.Order;
public record DeArchiveOrderCommand(Guid Id) {
    public static DeArchiveOrderCommand With(Guid id) {
        return new(id);
    }
}