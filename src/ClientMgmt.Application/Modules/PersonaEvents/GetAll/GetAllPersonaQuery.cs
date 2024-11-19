using ClientMgmt.Application.Modules.PersonaEvents.Common;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.GetAll;

public record GetAllPersonaQuery : IRequest<ErrorOr<IReadOnlyList<PersonaDto>>>
{

}