using Figure.DataAccess.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Figure.Application._Commands;
public record PatchOrderCommand(
    Guid Id,
    JsonPatchDocument<Order> JsonPatchDocument){
}

