namespace Figure.Application.Models;
public class ReadFigureModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string? FilePath { get; set; }
}