using AccountMgmt.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Create;

public record CreateCuentaCmd : IRequest<ErrorOr<Guid>>
{
    public string NumeroCuenta { get; set; } = string.Empty;
    public GenericEnums.TipoCuenta TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; } = true;
    public Guid ClienteId { get; set; }

}