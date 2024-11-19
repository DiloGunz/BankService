using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Create;

public class CreatePersonaCmdHandler : IRequestHandler<CreatePersonaCmd, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(CreatePersonaCmd request, CancellationToken cancellationToken)
    {
        var existIdentificacion = await _unitOfWork.Personas.AnyAsync(x => x.Identificacion == request.Identificacion.Trim());

        if (existIdentificacion)
        {
            return Error.Validation("Persona.Identificacion", "El código de identificación ya existe.");
        }

        var persona = new Persona
        {
            PersonaId = Guid.NewGuid(),
            Nombre = request.Nombre,
            Genero = request.Genero,
            Edad = request.Edad,
            Identificacion = request.Identificacion,
            Direccion = request.Direccion,
            Telefono = request.Telefono
        };

        await _unitOfWork.Personas.AddAsync(persona);
        await _unitOfWork.SaveChangesAsync();

        return persona.PersonaId;
    }
}