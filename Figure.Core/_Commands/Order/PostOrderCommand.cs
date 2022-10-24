namespace Figure.Application._Commands.Order;
public record PostOrderCommand(
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Description) {

}