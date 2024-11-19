using AccountMgmt.Application.Modules.MovimientoEvents.Common;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.GetById;

public record GetByIdMovimientoQuery : IRequest<ErrorOr<MovimientoDto>>
{
    public GetByIdMovimientoQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}