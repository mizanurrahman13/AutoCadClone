using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Circle : IShape
{
    public Point Center { get; }
    public double Radius { get; }

    public Circle(Point center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public void Draw(IDrawingContext context)
    {
        context.DrawCircle(Center.X, Center.Y, Radius);
    }
}
