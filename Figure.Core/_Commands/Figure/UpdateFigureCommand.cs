namespace Figure.Application._Commands.Figure;

public record UpdateFigureCommand(
    Guid Id,
    string Name,
    string Material,
    string Description,
    int Width,
    int Height,
    string FilePath){
    
}
