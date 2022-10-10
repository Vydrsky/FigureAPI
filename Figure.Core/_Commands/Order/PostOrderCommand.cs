namespace Figure.Application._Commands.Order;
public record PostOrderCommand(
    string Name,
    string Email,
    string PhoneNumber,
    string Description) {

}