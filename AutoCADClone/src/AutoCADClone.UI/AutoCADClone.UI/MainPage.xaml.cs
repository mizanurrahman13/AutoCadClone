using AutoCADClone.Application.Features.DrawAnyShape;
using AutoCADClone.Domain.Entities;
using MediatR;
using MauiPoint = Microsoft.Maui.Graphics.Point;
using DomainPoint = AutoCADClone.Domain.ValueObjects.Point;
using IShape = AutoCADClone.Domain.Shared.IShape;

namespace AutoCADClone.UI;

public partial class MainPage : ContentPage
{
    //private readonly IMediator _mediator;
    //private readonly CanvasService _canvasService;
    //private bool _isDrawingLine = false;
    //private DrawingMode _currentMode = DrawingMode.None;
    //private Point? _startPoint = null;

    //private void OnDrawLineClicked(object sender, EventArgs e)
    //{
    //    _currentMode = DrawingMode.Line;
    //    _startPoint = null;
    //}

    //private void OnDrawCircleClicked(object sender, EventArgs e)
    //{
    //    _currentMode = DrawingMode.Circle;
    //    _startPoint = null;
    //}

    //public MainPage(IMediator mediator, CanvasService canvasService)
    //{
    //    InitializeComponent();
    //    _mediator = mediator;
    //    _canvasService = canvasService;

    //    DrawingCanvas.StartInteraction += async (s, e) =>
    //    {
    //        var touch = e.Touches.FirstOrDefault();
    //        if (touch == null) return;

    //        var currentPoint = new Point(touch.X, touch.Y);

    //        switch (_currentMode)
    //        {
    //            case DrawingMode.Line:
    //                if (_startPoint == null)
    //                {
    //                    _startPoint = currentPoint;
    //                }
    //                else
    //                {
    //                    var line = await _mediator.Send(new DrawLineCommand(_startPoint.Value, currentPoint));
    //                    _canvasService.AddLine(line);
    //                    _startPoint = null;
    //                    DrawingCanvas.Invalidate();
    //                }
    //                break;

    //            case DrawingMode.Circle:
    //                if (_startPoint == null)
    //                {
    //                    _startPoint = currentPoint;
    //                }
    //                else
    //                {
    //                    var dx = currentPoint.X - _startPoint.Value.X;
    //                    var dy = currentPoint.Y - _startPoint.Value.Y;
    //                    var radius = Math.Sqrt(dx * dx + dy * dy);

    //                    var circle = await _mediator.Send(new DrawCircleCommand(_startPoint.Value, radius));
    //                    _canvasService.AddCircle(circle);
    //                    _startPoint = null;
    //                    DrawingCanvas.Invalidate();
    //                }
    //                break;

    //            default:
    //                break;
    //        }
    //    };

    //    DrawingCanvas.Drawable = new LineDrawable(_canvasService);
    //}

    private DrawingMode _currentMode = DrawingMode.None;
    private DomainPoint? _startPoint = null; // ✅ clear and correct

    private readonly IMediator _mediator;
    private readonly CanvasService _canvasService;
    private bool _isDrawingLine = false;
    private readonly List<DomainPoint> _polygonPoints = new();
    private readonly List<DomainPoint> _joinPoints = new();
    private readonly List<DomainPoint> _joinedLinePoints = new();


    public MainPage(IMediator mediator, CanvasService canvasService)
    {
        InitializeComponent();
        _mediator = mediator;
        _canvasService = canvasService;

        DrawingCanvas.Drawable = new CanvasDrawable(_canvasService);
        DrawingCanvas.StartInteraction += OnCanvasStartInteraction;
    }

    private void OnDrawLineClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Line;
        _startPoint = null;
    }

    private void OnDrawCircleClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Circle;
        _startPoint = null;
    }

    private void OnDrawRectangleClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Rectangle;
        _startPoint = null;
    }

    private void OnDrawHexagonClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Hexagon;
        _startPoint = null;
    }

    private void OnDrawPolygonClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Polygon;
        _polygonPoints.Clear();
    }

    private void OnJoinLinesClicked(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.JoinLines;
        _joinedLinePoints.Clear();
    }

    private DomainPoint ToDomainPoint(MauiPoint p) => new DomainPoint(p.X, p.Y);

    private async void OnCanvasStartInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches.FirstOrDefault();
        if (touch != null)
        {
            var domainPoint = ToDomainPoint(new MauiPoint(touch.X, touch.Y));
            if (_currentMode == DrawingMode.JoinLines)
            {
                _joinedLinePoints.Add(domainPoint);

                if (_joinedLinePoints.Count >= 2)
                {
                    var polyline = new Polyline(_joinedLinePoints);
                    var result = await _mediator.Send(new DrawAnyShapeCommand(polyline));
                    _canvasService.AddShape(result);

                    var bounds = GetShapePoints(polyline);
                    AdjustCanvasSize(bounds);

                    DrawingCanvas.Invalidate();
                    _joinedLinePoints.Clear();
                }
                return;
            }

            if (_currentMode == DrawingMode.Polygon)
            {
                _polygonPoints.Add(domainPoint);
                if (_polygonPoints.Count >= 3)
                {
                    await HandleShapeDrawing(domainPoint);
                    _polygonPoints.Clear();
                }
            }
            else if (_startPoint == null)
            {
                _startPoint = domainPoint;
            }
            else
            {
                await HandleShapeDrawing(domainPoint);
            }

        }
    }


    private async Task HandleShapeDrawing(DomainPoint currentPoint)
    {
        IShape shape = _currentMode switch
        {
            DrawingMode.Line => new Line(_startPoint.Value, currentPoint),
            DrawingMode.Circle => new Circle(_startPoint.Value, CalculateRadius(_startPoint.Value, currentPoint)),
            DrawingMode.Rectangle => new Rectangle(_startPoint.Value, currentPoint),
            DrawingMode.Hexagon => new Hexagon(_startPoint.Value, CalculateRadius(_startPoint.Value, currentPoint)),
            DrawingMode.Polygon => new Polygon(_polygonPoints),
            _ => null
        };

        if (shape != null)
        {
            var result = await _mediator.Send(new DrawAnyShapeCommand(shape));
            _canvasService.AddShape(result);

            // 🔍 Cutoff logic: expand canvas if needed
            var bounds = GetShapePoints(shape);
            AdjustCanvasSize(bounds);

            DrawingCanvas.Invalidate();
        }

        _startPoint = null;
    }


    private double CalculateRadius(DomainPoint a, DomainPoint b)
    {
        var dx = b.X - a.X;
        var dy = b.Y - a.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private void AdjustCanvasSize(IEnumerable<DomainPoint> points)
    {
        if (points == null || !points.Any())
        {
            Console.WriteLine("⚠️ AdjustCanvasSize called with empty point list.");

            return; // ⛔ Prevents crash when no points are passed
        }            

        double margin = 100;
        double maxX = points.Max(p => p.X) + margin;
        double maxY = points.Max(p => p.Y) + margin;

        if (DrawingCanvas.WidthRequest < maxX)
            DrawingCanvas.WidthRequest = maxX;

        if (DrawingCanvas.HeightRequest < maxY)
            DrawingCanvas.HeightRequest = maxY;
    }

    private IEnumerable<DomainPoint> GetShapePoints(IShape shape)
    {
        return shape switch
        {
            Line line => new[] { line.Start, line.End },
            Rectangle rect => new[] { rect.TopLeft, rect.BottomRight },
            Circle circle => new[] {
            new DomainPoint(circle.Center.X + circle.Radius, circle.Center.Y + circle.Radius),
            new DomainPoint(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius)
        },
            Hexagon hex => Enumerable.Range(0, 6).Select(i =>
            {
                double angle = Math.PI / 3 * i;
                return new DomainPoint(
                    hex.Center.X + hex.Radius * Math.Cos(angle),
                    hex.Center.Y + hex.Radius * Math.Sin(angle));
            }),
            Polygon poly => poly.Vertices,
            _ => Enumerable.Empty<DomainPoint>()
        };
    }

    public enum DrawingMode
    {
        None,
        Line,
        Circle,
        Rectangle,
        Hexagon,
        Polygon,
        JoinLines
    }
}
