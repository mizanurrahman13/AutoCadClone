using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Polygon : IShape
{
    public List<Point> Vertices { get; }

    public Polygon(IEnumerable<Point> vertices)
    {
        Vertices = vertices.ToList();
    }

    public void Draw(IDrawingContext context)
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            var p1 = Vertices[i];
            var p2 = Vertices[(i + 1) % Vertices.Count];
            context.DrawLine(p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
