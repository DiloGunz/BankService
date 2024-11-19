using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ClientMgmt.Application.Modules.PersonaEvents.Create;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.ClienteEvents.Create;

/// <summary>
/// Comando para crear un nuevo cliente.
/// Contiene la información del cliente y de la persona asociada que serán creados.
/// </summary>
public record CreateClienteCmd : IRequest<ErrorOr<CreateClienteResponse>>
{
    /// <summary>
    /// Identificador único del cliente.
    /// </summary>
    public Guid ClienteId { get; set; }

    /// <summary>
    /// Identificador único de la persona asociada al cliente.
    /// </summary>
    public Guid PersonaId { get; set; }

    /// <summary>
    /// Contraseña del cliente.
    /// </summary>
    public string Contraseña { get; set; } = string.Empty;

    /// <summary>
    /// Estado del cliente (activo o inactivo).
    /// </summary>
    public bool Estado { get; set; } = true;

    /// <summary>
    /// Comando que contiene la información de la persona asociada.
    /// </summary>
    public CreatePersonaCmd Persona { get; set; }
}
