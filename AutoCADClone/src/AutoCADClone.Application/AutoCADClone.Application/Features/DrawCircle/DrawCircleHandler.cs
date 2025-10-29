using AutoCADClone.Domain.Entities;
using MediatR;

namespace AutoCADClone.Application.Features.DrawCircle;

public class DrawCircleHandler : IRequestHandler<DrawCircleCommand, Circle>
{
    public Task<Circle> Handle(DrawCircleCommand request, CancellationToken cancellationToken)
    {
        var circle = new Circle(request.Center, request.Radius);
        return Task.FromResult(circle);
    }
}