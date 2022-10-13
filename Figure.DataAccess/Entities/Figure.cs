using Figure.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Figure.DataAccess.Entities;

public class Figure : IEntity{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string? FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
}

