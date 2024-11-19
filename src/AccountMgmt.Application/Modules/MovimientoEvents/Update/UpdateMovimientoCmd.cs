using AccountMgmt.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Update;

public record UpdateMovimientoCmd : IRequest<ErrorOr<Unit>>
{
    public Guid MovimientoId { get; set; } 
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
}