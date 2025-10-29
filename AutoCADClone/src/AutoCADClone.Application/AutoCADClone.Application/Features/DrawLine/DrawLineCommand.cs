using AutoCADClone.Domain.Entities;
using MediatR;
using Point = AutoCADClone.Domain.ValueObjects.Point;

namespace AutoCADClone.Application.Features.DrawLine;

public record DrawLineCommand(Point Start, Point End) : IRequest<Line>;
