using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetSaldo;

public record GetSaldoCuentaQuery(Guid CuentaId) : IRequest<ErrorOr<GetSaldoCuentaResponse>>
{
}
