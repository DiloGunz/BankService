namespace Api.Gateway.WebClient.Models.AccountMgmt.Common;

/// <summary>
/// Clase que contiene enumeradores genéricos utilizados en el dominio.
/// </summary>
public class GenericEnums
{
    /// <summary>
    /// Representa los tipos de cuenta disponibles.
    /// Ahorros = 1, Corriente = 2.
    /// </summary>
    public enum TipoCuenta
    {
        /// <summary>
        /// Cuenta de ahorros.
        /// </summary>
        Ahorros = 1,
        /// <summary>
        /// Cuenta corriente.
        /// </summary>
        Corriente = 2
    }

    /// <summary>
    /// Representa los tipos de movimiento que se pueden realizar en una cuenta.
    /// Depósito = 1, Retiro = 2.
    /// </summary>
    public enum TipoMovimiento
    {
        /// <summary>
        /// Movimiento de depósito.
        /// </summary>
        Deposito = 1,

        /// <summary>
        /// Movimiento de retiro.
        /// </summary>
        Retiro = 2
    }

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