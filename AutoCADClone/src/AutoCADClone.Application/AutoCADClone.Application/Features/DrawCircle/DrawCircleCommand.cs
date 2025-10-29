using AutoCADClone.Domain.Entities;
using AutoCADClone.Domain.ValueObjects;
using MediatR;

namespace AutoCADClone.Application.Features.DrawCircle;

public record DrawCircleCommand(Point Center, double Radius) : IRequest<Circle>;
