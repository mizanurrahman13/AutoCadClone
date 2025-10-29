using AutoCADClone.Domain.Entities;
using AutoCADClone.Domain.ValueObjects;
using MediatR;

namespace AutoCADClone.Application.Features.DrawLine;

public class DrawLineHandler : IRequestHandler<DrawLineCommand, Line>
{
    public Task<Line> Handle(DrawLineCommand request, CancellationToken cancellationToken)
    {
        var line = new Line(request.Start, request.End);
        return Task.FromResult(line);
    }
}