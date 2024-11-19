using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.CreateByNroCuenta;

public record CreateMovimientoByNroCuentaCmd : IRequest<ErrorOr<Guid>>
{
    public decimal Valor { get; set; }
    public string NumeroCuenta { get; set; }
}