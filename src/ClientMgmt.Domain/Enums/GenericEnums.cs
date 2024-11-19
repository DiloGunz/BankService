namespace ClientMgmt.Domain.Enums;

/// <summary>
/// Clase que contiene enumeradores genéricos utilizados en el dominio.
/// </summary>
public class GenericEnums
{
    /// <summary>
    /// Representa los géneros posibles para una persona.
    /// NoEspecificado = 0, Masculino = 1, Femenino = 2.
    /// </summary>
    public enum GeneroPersona
    {
        /// <summary>
        /// Género no especificado.
        /// </summary>
        NoEspecificado = 0,

        /// <summary>
        /// Género masculino.
        /// </summary>
        Masculino = 1,

        /// <summary>
        /// Género femenino.
        /// </summary>
        Femenino = 2
    }
}