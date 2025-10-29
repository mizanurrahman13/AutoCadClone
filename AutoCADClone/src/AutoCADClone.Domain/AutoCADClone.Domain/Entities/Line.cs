using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Line : IShape
{
    public Point Start { get; }
    public Point End { get; }

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public void Draw(IDrawingContext context)
    {
        context.DrawLine(Start.X, Start.Y, End.X, End.Y);
    }
}
