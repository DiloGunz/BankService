using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Delete;

public class DeletePersonaCmdHandler : IRequestHandler<DeletePersonaCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePersonaCmd request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(request.PersonaId);

        if (persona is not Persona)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        _unitOfWork.Personas.Remove(persona);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}