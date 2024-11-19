using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Update;

/// <summary>
/// Manejador para procesar el comando UpdateClienteCmd.
/// Se encarga de actualizar un cliente y su persona asociada en el sistema.
/// </summary>
public class UpdateClienteCmdHandler : IRequestHandler<UpdateClienteCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor de UpdateClienteCmdHandler.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo para interactuar con la capa de datos.</param>
    /// <param name="mediator">Mediador para enviar comandos relacionados.</param>
    /// <exception cref="ArgumentNullException">Lanzada si unitOfWork o mediator es nulo.</exception>
    public UpdateClienteCmdHandler(IUnitOfWork unitOfWork, ISender mediator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Maneja el comando para actualizar un cliente y su información asociada.
    /// </summary>
    /// <param name="request">Comando que contiene los datos del cliente a actualizar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Un resultado de tipo Unit si la actualización es exitosa, o un error si falla.</returns>
    public async Task<ErrorOr<Unit>> Handle(UpdateClienteCmd request, CancellationToken cancellationToken)
    {
        // Recuperar el cliente por su ID
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(request.ClienteId);

        // Validar si el cliente existe
        if (cliente is not Cliente)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        // Iniciar una transacción para garantizar consistencia
        using var trx = await _unitOfWork.BeginTransactionAsync();

        // Actualizar las propiedades del cliente
        cliente.Estado = request.Estado;
        cliente.Contraseña = request.Contraseña;

        // Actualizar el cliente en la base de datos
        _unitOfWork.Clientes.Update(cliente);
        await _unitOfWork.SaveChangesAsync();

        // Enviar el comando para actualizar la información de la persona asociada
        var updatePersonaResponse = await _mediator.Send(request.Persona);

        // Validar si la actualización de la persona generó errores
        if (updatePersonaResponse.IsError)
        {
            return updatePersonaResponse.Errors;
        }

        // Confirmar la transacción si todo fue exitoso
        await trx.CommitAsync();

        // Devolver un resultado exitoso
        return Unit.Value;
    }
}
