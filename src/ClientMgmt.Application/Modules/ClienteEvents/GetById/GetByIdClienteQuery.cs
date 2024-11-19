using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.GetById;

/// <summary>
/// Consulta para obtener un cliente específico por su ID.
/// </summary>
/// <remarks>
/// Proporciona el identificador único del cliente y devuelve un objeto ClienteDto o un error si no se encuentra.
/// </remarks>
public record GetByIdClienteQuery : IRequest<ErrorOr<ClienteDto>>
{
    /// <summary>
    /// Constructor de la consulta GetByIdClienteQuery.
    /// </summary>
    /// <param name="id">Identificador único del cliente.</param>
    public GetByIdClienteQuery(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Identificador único del cliente que se desea obtener.
    /// </summary>
    public Guid Id { get; set; }
}
