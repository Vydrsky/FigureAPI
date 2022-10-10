namespace Figure.Application._Commands;
public record DeleteOrderCommand(Guid Id) {
    public static DeleteOrderCommand With(Guid id) {
        return new(id);
    }
}