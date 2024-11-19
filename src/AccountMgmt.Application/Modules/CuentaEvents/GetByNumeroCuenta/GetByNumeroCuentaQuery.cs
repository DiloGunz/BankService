using AccountMgmt.Application.Modules.CuentaEvents.Common;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetByNumeroCuenta;

public record GetByNumeroCuentaQuery : IRequest<ErrorOr<CuentaDto>>
{
	public GetByNumeroCuentaQuery(string numeroCuenta)
	{
		NumeroCuenta = numeroCuenta;
	}
    public string NumeroCuenta { get; set; }
}