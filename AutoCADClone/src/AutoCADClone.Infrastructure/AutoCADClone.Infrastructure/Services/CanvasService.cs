//using AutoCADClone.Domain.Entities;

//namespace AutoCADClone.Infrastructure.Services;

//public class CanvasService
//{
//    private readonly List<Line> _lines = new();
//    private readonly List<Circle> _circles = new();

//    public void AddLine(Line line)
//    {
//        _lines.Add(line);
//    }

//    public IEnumerable<Line> GetLines() => _lines;

//    public void AddCircle(Circle circle)
//    {
//        _circles.Add(circle);
//    }

//    public IEnumerable<Circle> GetCircles() => _circles;    
//}

using AutoCADClone.Domain.Shared;

public class CanvasService
{
    private readonly List<IShape> _shapes = new();

    public void AddShape(IShape shape) => _shapes.Add(shape);

    public IEnumerable<IShape> GetShapes() => _shapes;
}
