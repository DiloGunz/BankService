using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.EstadoCuenta;

public record EstadoCuentaQuery : IRequest<ErrorOr<IReadOnlyList<EstadoCuentaDto>>>
{
    public DateTimeOffset FechaInicio { get; set; }
    public DateTimeOffset FechaFin { get; set; }
    public Guid ClienteId { get; set; }
}