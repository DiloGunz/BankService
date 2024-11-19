using ClientMgmt.Application.Modules.PersonaEvents.Delete;
using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.Delete;

/// <summary>
/// Manejador para procesar el comando DeleteClienteCmd.
/// Se encarga de eliminar un cliente del sistema.
/// </summary>
public class DeleteClienteCmdHandler : IRequestHandler<DeleteClienteCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor de DeleteClienteCmdHandler.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo para interactuar con la capa de datos.</param>
    /// <param name="mediator">Mediador para enviar otros comandos o consultas.</param>
    /// <exception cref="ArgumentNullException">Lanzada si unitOfWork o mediator es nulo.</exception>
    public DeleteClienteCmdHandler(IUnitOfWork unitOfWork, ISender mediator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Maneja el comando para eliminar un cliente.
    /// </summary>
    /// <param name="request">Comando que contiene el ID del cliente a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Un resultado de tipo Unit si la eliminación es exitosa, o un error si falla.</returns>
    public async Task<ErrorOr<Unit>> Handle(DeleteClienteCmd request, CancellationToken cancellationToken)
    {
        // Iniciar una transacción para garantizar la consistencia de datos
        using var trx = await _unitOfWork.BeginTransactionAsync();

        // Obtener el cliente por su ID
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(request.ClienteId);

        // Validar si el cliente existe
        if (cliente is not Cliente)
        {
            // Devolver un error de tipo "No encontrado" si el cliente no existe
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        // Eliminar el cliente del contexto de datos
        _unitOfWork.Clientes.Remove(cliente);

        // Guardar los cambios en la base de datos
        await _unitOfWork.SaveChangesAsync();

        // Enviar un comando para eliminar también la entidad Persona asociada al cliente
        var personaResult = await _mediator.Send(new DeletePersonaCmd(cliente.PersonaId));

        // Verificar si hubo errores al eliminar la Persona
        if (personaResult.IsError)
        {
            // Si hubo errores, devolverlos al llamador
            return personaResult.Errors;
        }

        // Confirmar la transacción si todo fue exitoso
        await trx.CommitAsync();

        // Devolver un resultado exitoso
        return Unit.Value;
    }
}
