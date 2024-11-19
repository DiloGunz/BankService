using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Delete;

public record DeleteMovimientoCmd : IRequest<ErrorOr<Unit>>
{
    public DeleteMovimientoCmd(Guid id)
    {
        MovimientoId = id;
    }
    public Guid MovimientoId { get; set; }
}