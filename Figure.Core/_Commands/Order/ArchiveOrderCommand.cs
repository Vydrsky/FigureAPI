using Figure.Infrastructure;

namespace Figure.Application._Commands.Order;
public record ArchiveOrderCommand(Guid Id){
    public static ArchiveOrderCommand With(Guid id) {
        return new(id);
    }
}