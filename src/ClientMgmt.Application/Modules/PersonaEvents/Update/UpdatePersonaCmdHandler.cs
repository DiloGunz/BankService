using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Update;

public class UpdatePersonaCmdHandler : IRequestHandler<UpdatePersonaCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdatePersonaCmd request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(request.PersonaId);

        if (persona is not Persona)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        var existIdentificacion = await _unitOfWork.Personas
            .AnyAsync(x => x.Identificacion == request.Identificacion && x.PersonaId != request.PersonaId);

        if (existIdentificacion)
        {
            return Error.Validation("Persona.Identification", "El número de identificación ya existe.");
        }

        persona.Direccion = request.Direccion;
        persona.Identificacion = request.Identificacion;
        persona.Edad = request.Edad; 
        persona.Genero = request.Genero;
        persona.Nombre = request.Nombre;
        persona.Telefono = request.Telefono;

        _unitOfWork.Personas.Update(persona);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
