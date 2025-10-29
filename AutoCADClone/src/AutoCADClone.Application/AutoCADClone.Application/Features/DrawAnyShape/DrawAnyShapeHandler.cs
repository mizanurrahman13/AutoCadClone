using AutoCADClone.Domain.Shared;
using MediatR;

namespace AutoCADClone.Application.Features.DrawAnyShape;

public class DrawAnyShapeHandler : IRequestHandler<DrawAnyShapeCommand, IShape>
{
    public Task<IShape> Handle(DrawAnyShapeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.Shape);
    }
}

