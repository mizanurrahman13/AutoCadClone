using AutoCADClone.Domain.Shared;
using global::AutoCADClone.Domain.Shared;
using MediatR;

namespace AutoCADClone.Application.Features.DrawAnyShape;

public record DrawAnyShapeCommand(IShape Shape) : IRequest<IShape>;

