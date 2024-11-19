using AccountMgmt.Application.Modules.CuentaEvents.Common;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetAll;

public record GetAllCuentaQuery : IRequest<ErrorOr<IReadOnlyList<CuentaDto>>>
{

}