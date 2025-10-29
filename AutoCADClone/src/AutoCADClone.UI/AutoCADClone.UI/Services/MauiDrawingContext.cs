using AutoCADClone.Domain.Shared;
using Microsoft.Maui.Graphics;

namespace AutoCADClone.UI.Services;

public class MauiDrawingContext : IDrawingContext
{
    private readonly ICanvas _canvas;

    public MauiDrawingContext(ICanvas canvas)
    {
        _canvas = canvas;
    }

    public void DrawLine(double x1, double y1, double x2, double y2)
        => _canvas.DrawLine((float)x1, (float)y1, (float)x2, (float)y2);

    public void DrawCircle(double centerX, double centerY, double radius)
        => _canvas.DrawCircle((float)centerX, (float)centerY, (float)radius);

    public void DrawRectangle(double x, double y, double width, double height)
        => _canvas.DrawRectangle((float)x, (float)y, (float)width, (float)height);

    public void DrawHexagon(double centerX, double centerY, double radius)
    {
        var points = new List<(double x, double y)>();
        for (int i = 0; i < 6; i++)
        {
            double angle = Math.PI / 3 * i;
            double x = centerX + radius * Math.Cos(angle);
            double y = centerY + radius * Math.Sin(angle);
            points.Add((x, y));
        }

        DrawPolygon(points);
    }

    public void DrawPolygon(IEnumerable<(double x, double y)> points)
    {
        var pointList = points.ToList();
        for (int i = 0; i < pointList.Count; i++)
        {
            var p1 = pointList[i];
            var p2 = pointList[(i + 1) % pointList.Count];
            DrawLine(p1.x, p1.y, p2.x, p2.y);
        }
    }

    public void DrawPolyline(IEnumerable<(double x, double y)> points)
    {
        var pointList = points.ToList();
        if (pointList.Count < 2)
            return;

        for (int i = 0; i < pointList.Count - 1; i++)
        {
            var p1 = pointList[i];
            var p2 = pointList[i + 1];
            DrawLine(p1.x, p1.y, p2.x, p2.y);
        }
    }
}

