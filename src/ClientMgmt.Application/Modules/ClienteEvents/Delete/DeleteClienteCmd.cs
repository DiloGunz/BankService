using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.Delete;

/// <summary>
/// Comando para eliminar un cliente.
/// Contiene el identificador único del cliente que se desea eliminar.
/// </summary>
public record DeleteClienteCmd : IRequest<ErrorOr<Unit>>
{
    /// <summary>
    /// Constructor del comando DeleteClienteCmd.
    /// </summary>
    /// <param name="id">Identificador único del cliente.</param>
    public DeleteClienteCmd(Guid id)
    {
        ClienteId = id;
    }

    /// <summary>
    /// Identificador único del cliente que se desea eliminar.
    /// </summary>
    public Guid ClienteId { get; set; }
}
