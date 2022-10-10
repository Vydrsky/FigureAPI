namespace Figure.Application._Commands.Order;
public record UpdateOrderCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Description) {

}
