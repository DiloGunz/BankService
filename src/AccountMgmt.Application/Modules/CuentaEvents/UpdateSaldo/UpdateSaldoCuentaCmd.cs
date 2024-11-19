using AccountMgmt.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.UpdateSaldo;

public record UpdateSaldoCuentaCmd : IRequest<ErrorOr<Unit>>
{
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
    public Guid CuentaId { get; set; }
}