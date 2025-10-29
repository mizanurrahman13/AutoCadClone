namespace AutoCADClone.Domain.Shared;

public interface IDrawingContext
{
    void DrawLine(double x1, double y1, double x2, double y2);
    void DrawCircle(double centerX, double centerY, double radius);
    void DrawRectangle(double x, double y, double width, double height);
    void DrawHexagon(double centerX, double centerY, double radius);
    void DrawPolygon(IEnumerable<(double x, double y)> points);
    void DrawPolyline(IEnumerable<(double x, double y)> points);

}