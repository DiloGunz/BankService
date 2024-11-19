using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.Create;

/// <summary>
/// Manejador para procesar el comando CreateClienteCmd.
/// Se encarga de validar y crear un nuevo cliente en el sistema.
/// </summary>
public class CreateClienteCmdHandler : IRequestHandler<CreateClienteCmd, ErrorOr<CreateClienteResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor de CreateClienteCmdHandler.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo para interactuar con la capa de datos.</param>
    /// <exception cref="ArgumentNullException">Lanzada si unitOfWork es nulo.</exception>
    public CreateClienteCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Maneja el comando para crear un cliente.
    /// </summary>
    /// <param name="request">Comando que contiene los datos del cliente a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Una respuesta con el ID del cliente creado o un error de validación.</returns>
    public async Task<ErrorOr<CreateClienteResponse>> Handle(CreateClienteCmd request, CancellationToken cancellationToken)
    {
        // Validar si ya existe una persona con la misma identificación
        var existIdentificacion = await _unitOfWork.Personas.AnyAsync(x => x.Identificacion == request.Persona.Identificacion.Trim());

        if (existIdentificacion)
        {
            // Devolver un error de validación si la identificación ya existe
            return Error.Validation("Persona.Identificacion", "El código de identificación ya existe.");
        }

        // Crear la entidad Cliente junto con su entidad Persona asociada
        var cliente = new Cliente()
        {
            Persona = new Persona()
            {
                Nombre = request.Persona.Nombre,
                Genero = request.Persona.Genero,
                Edad = request.Persona.Edad,
                Identificacion = request.Persona.Identificacion,
                Direccion = request.Persona.Direccion,
                Telefono = request.Persona.Telefono
            },
            Contraseña = request.Contraseña,
            Estado = request.Estado
        };

        // Agregar el cliente al contexto de datos y guardar los cambios
        await _unitOfWork.Clientes.AddAsync(cliente);
        await _unitOfWork.SaveChangesAsync();

        // Crear la respuesta con los IDs del cliente y su persona asociada
        var response = new CreateClienteResponse(cliente.ClienteId, cliente.PersonaId);

        return response;
    }
}