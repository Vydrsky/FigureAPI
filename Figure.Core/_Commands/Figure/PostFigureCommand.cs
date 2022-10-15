namespace Figure.Application._Commands.Figure;

public record PostFigureCommand(
    string Name,
    string Material,
    string Description,
    int Width,
    int Height,
    string FilePath){
    
}
