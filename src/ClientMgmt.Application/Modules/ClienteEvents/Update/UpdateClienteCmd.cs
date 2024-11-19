using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.Update;

/// <summary>
/// Comando para actualizar los datos de un cliente.
/// Contiene la información del cliente y de la persona asociada que serán actualizados.
/// </summary>
public record UpdateClienteCmd : IRequest<ErrorOr<Unit>>
{
    /// <summary>
    /// Identificador único del cliente a actualizar.
    /// </summary>
    public Guid ClienteId { get; set; }

    /// <summary>
    /// Comando que contiene los datos actualizados de la persona asociada.
    /// </summary>
    public UpdatePersonaCmd Persona { get; set; }

    /// <summary>
    /// Nueva contraseña del cliente.
    /// </summary>
    public string Contrasena { get; set; } = string.Empty;

    /// <summary>
    /// Nuevo estado del cliente (activo o inactivo).
    /// </summary>
    public bool Estado { get; set; } = true;
}
