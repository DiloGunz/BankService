using AccountMgmt.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Create;

public record CreateMovimientoCmd : IRequest<ErrorOr<Guid>>
{
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
    public Guid CuentaId { get; set; }
}