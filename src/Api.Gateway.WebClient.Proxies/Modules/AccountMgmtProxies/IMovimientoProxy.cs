using Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.Commands;

namespace Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies;

/// <summary>
/// Interfaz que define las operaciones para interactuar con el servicio de movimientos.
/// Proporciona métodos para realizar operaciones CRUD utilizando solicitudes HTTP.
/// </summary>
public interface IMovimientoProxy
{
    /// <summary>
    /// Obtiene todos los movimientos.
    /// </summary>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> GetAllAsync();

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> GetByIdAsync(Guid id);

    /// <summary>
    /// Crea un nuevo movimiento.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear el movimiento.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> CreateAsync(CreateMovimientoCmd command);

    /// <summary>
    /// Crea un nuevo movimiento utilizando el numero de cuenta y no su ID.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear el movimiento.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> CreateByNroCuentaAsync(CreateMovimientoByNroCuentaCmd command);

    /// <summary>
    /// Actualiza un movimiento existente.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a actualizar.</param>
    /// <param name="command">Comando que contiene los datos actualizados del movimiento.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateMovimientoCmd command);

    /// <summary>
    /// Elimina un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a eliminar.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> DeleteAsync(Guid id);
}
