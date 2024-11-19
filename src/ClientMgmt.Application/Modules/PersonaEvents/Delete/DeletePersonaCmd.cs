using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Delete;

public record DeletePersonaCmd : IRequest<ErrorOr<Unit>>
{
	public DeletePersonaCmd(Guid id)
	{
		PersonaId = id;
	}
    public Guid PersonaId { get; set; }
}