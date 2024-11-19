namespace ClientMgmt.Domain.Entities;

/// <summary>
/// Entidad del dominio: Cliente
/// Representa la entidad cliente en el dominio, la cual está relacionada con una persona.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único del cliente (clave primaria).
    /// </summary>
    public Guid ClienteId { get; set; }
    /// <summary>
    /// Relación de cliente con la persona asociada (clave foránea).
    /// </summary>
    public Guid PersonaId { get; set; }
    /// <summary>
    /// Contraseña del cliente (requiere manejo seguro en producción, como encriptación).
    /// </summary>
    public string Contraseña { get; set; } = string.Empty;
    /// <summary>
    /// Indica si el cliente está activo (true) o inactivo (false).
    /// </summary>
    public bool Estado { get; set; } = true;
    /// <summary>
    /// Propiedad de navegación: Relación uno a uno con Persona
    /// </summary>
    public Persona? Persona { get; set; }
}