using Figure.DataAccess.Entities;

namespace Figure.Tests.Builders;
internal class OrderBuilder {
    private Order _order = new();

    public Order Build() {
        return _order;
    }

    public OrderBuilder AddId(Guid Id) {
        _order.Id = Id;
        return this;
    }

    public OrderBuilder AddName(string Name) {
        _order.Name = Name;
        return this;
    }

    public OrderBuilder AddEmail(string Email) {
        _order.Email = Email;
        return this;
    }

    public OrderBuilder AddPhoneNumber(string PhoneNumber) {
        _order.PhoneNumber = PhoneNumber;
        return this;
    }

    public OrderBuilder AddDescription(string Description) {
        _order.Description = Description;
        return this;
    }

    public OrderBuilder AddIsArchived(bool IsArchived) {
        _order.IsArchived = IsArchived;
        return this;
    }

    public OrderBuilder AddArchivedAt(DateTime ArchivedAt) {
        _order.ArchivedAt = ArchivedAt;
        return this;
    }
}

