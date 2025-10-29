using AutoCADClone.Domain.Shared;
using AutoCADClone.Domain.ValueObjects;

namespace AutoCADClone.Domain.Entities;

public class Rectangle : IShape
{
    public Point TopLeft { get; }
    public Point BottomRight { get; }

    public Rectangle(Point topLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }

    public void Draw(IDrawingContext context)
    {
        var x = TopLeft.X;
        var y = TopLeft.Y;
        var width = BottomRight.X - TopLeft.X;
        var height = BottomRight.Y - TopLeft.Y;

        context.DrawRectangle(x, y, width, height);
    }
}