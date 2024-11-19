namespace AccountMgmt.Domain.Enums;

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
}