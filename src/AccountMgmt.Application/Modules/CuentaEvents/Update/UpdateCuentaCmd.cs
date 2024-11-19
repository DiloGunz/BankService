using AccountMgmt.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Update;

public record UpdateCuentaCmd : IRequest<ErrorOr<Unit>>
{
    public Guid CuentaId { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public GenericEnums.TipoCuenta TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; } = true;
}