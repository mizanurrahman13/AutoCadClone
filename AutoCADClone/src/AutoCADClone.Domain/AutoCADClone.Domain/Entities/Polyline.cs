using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Polyline : IShape
{
    public List<Point> Points { get; }

    public Polyline(IEnumerable<Point> points)
    {
        Points = points.ToList();
    }

    public void Draw(IDrawingContext context)
    {
        for (int i = 0; i < Points.Count - 1; i++)
        {
            var p1 = Points[i];
            var p2 = Points[i + 1];
            context.DrawLine(p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
