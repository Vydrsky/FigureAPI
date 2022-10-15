namespace Figure.Application.Models;
public class ReadOrderModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    public bool IsArchived { get; set; }
}
