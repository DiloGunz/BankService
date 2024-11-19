using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.GetAll;

/// <summary>
/// Consulta para obtener la lista de todos los clientes.
/// </summary>
/// <remarks>
/// Esta consulta no requiere parámetros adicionales y devuelve una lista de objetos ClienteDto.
/// </remarks>
public record GetAllClienteQuery : IRequest<ErrorOr<IReadOnlyList<ClienteDto>>>
{
    // No se requieren propiedades ni parámetros adicionales para esta consulta.
}
