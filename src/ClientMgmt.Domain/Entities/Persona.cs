using ClientMgmt.Domain.Enums;

namespace ClientMgmt.Domain.Entities;
/// <summary>
/// Entidad del dominio: Persona
/// Representa la información personal asociada a un cliente.
/// </summary>
public class Persona
{
    /// <summary>
    /// Identificador único de la persona (clave primaria).
    /// </summary>
    public Guid PersonaId { get; set; }
    /// <summary>
    /// Nombre completo de la persona.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;
    /// <summary>
    /// Género de la persona representado mediante un enumerador.
    /// </summary>
    public GenericEnums.GeneroPersona Genero { get; set; }
    /// <summary>
    /// Edad de la persona en años.
    /// </summary>
    public int Edad { get; set; }
    /// <summary>
    /// Identificación única de la persona (puede ser cédula, DNI, etc.).
    /// </summary>
    public string Identificacion { get; set; } = string.Empty;
    /// <summary>
    /// Dirección de residencia de la persona.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;
    /// <summary>
    /// Número de teléfono de la persona.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
    /// <summary>
    /// Propiedad de navegación: Relación uno a uno con Cliente.
    /// </summary>
    public Cliente? Cliente { get; set; }
}