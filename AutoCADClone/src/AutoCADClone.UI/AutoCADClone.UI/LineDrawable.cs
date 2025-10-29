//using System;
//using AutoCADClone.Infrastructure.Services;
//using Microsoft.Maui.Graphics;

//namespace AutoCADClone.UI
//{
//    public class LineDrawable : IDrawable
//    {
//        private readonly CanvasService _canvasService;

//        public LineDrawable(CanvasService canvasService)
//        {
//            _canvasService = canvasService;
//        }

//        public void Draw(ICanvas canvas, RectF dirtyRect)
//        {
//            foreach (var line in _canvasService.GetLines())
//            {
//                canvas.DrawLine((float)line.Start.X, (float)line.Start.Y, (float)line.End.X, (float)line.End.Y);
//            }

//            foreach (var circle in _canvasService.GetCircles())
//            {
//                canvas.DrawCircle((float)circle.Center.X, (float)circle.Center.Y,
//                                  (float)circle.Radius);
//            }
//        }
//    }
//}

using AutoCADClone.UI.Services;

public class CanvasDrawable : IDrawable
{
    private readonly CanvasService _canvasService;

    public CanvasDrawable(CanvasService canvasService)
    {
        _canvasService = canvasService;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var context = new MauiDrawingContext(canvas);
        foreach (var shape in _canvasService.GetShapes())
        {
            shape.Draw(context);
        }
    }
}