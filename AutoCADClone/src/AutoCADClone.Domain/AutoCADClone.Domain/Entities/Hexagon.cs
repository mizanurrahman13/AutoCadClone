using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Hexagon : IShape
{
    public Point Center { get; }
    public double Radius { get; }

    public Hexagon(Point center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public void Draw(IDrawingContext context)
    {
        var points = new List<Point>();
        for (int i = 0; i < 6; i++)
        {
            double angle = Math.PI / 3 * i;
            double x = Center.X + Radius * Math.Cos(angle);
            double y = Center.Y + Radius * Math.Sin(angle);
            points.Add(new Point(x, y));
        }

        for (int i = 0; i < 6; i++)
        {
            var p1 = points[i];
            var p2 = points[(i + 1) % 6];
            context.DrawLine(p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
