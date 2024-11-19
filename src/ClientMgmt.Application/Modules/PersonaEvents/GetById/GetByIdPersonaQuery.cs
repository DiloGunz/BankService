using ClientMgmt.Application.Modules.PersonaEvents.Common;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.GetById;

public record GetByIdPersonaQuery : IRequest<ErrorOr<PersonaDto>>
{
	public GetByIdPersonaQuery(Guid id)
	{
		Id = id;
	}
    public Guid Id { get; set; }
}