using Figure.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Figure.DataAccess.Entities;
public class Order : IEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Surname { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    
    public string? Description { get; set; }    // will have to be changed based on requirements

    public DateTime CreatedAt { get; set; }

    public bool IsArchived { get; set; }

    public DateTime? ArchivedAt { get; set; }
}
