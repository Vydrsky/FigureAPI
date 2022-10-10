using Microsoft.AspNetCore.JsonPatch;

namespace Figure.Application._Commands.Order;
public record PatchOrderCommand(
    Guid Id,
    JsonPatchDocument<DataAccess.Entities.Order> JsonPatchDocument) {
}

