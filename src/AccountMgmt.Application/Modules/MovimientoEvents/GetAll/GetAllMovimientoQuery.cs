using AccountMgmt.Application.Modules.MovimientoEvents.Common;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.GetAll;

public record GetAllMovimientoQuery : IRequest<ErrorOr<IReadOnlyList<MovimientoDto>>>
{

}