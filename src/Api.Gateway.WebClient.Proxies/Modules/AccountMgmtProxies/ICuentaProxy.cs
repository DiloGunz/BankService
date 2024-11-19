using Api.Gateway.WebClient.Models.AccountMgmt.Cuenta.Commands;

namespace Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies;

/// <summary>
/// Interfaz que define las operaciones para interactuar con el servicio de cuentas.
/// Proporciona métodos para realizar operaciones CRUD utilizando solicitudes HTTP.
/// </summary>
public interface ICuentaProxy
{
    /// <summary>
    /// Obtiene todas las cuentas.
    /// </summary>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> GetAllAsync();

    /// <summary>
    /// Obtiene una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> GetByIdAsync(Guid id);

    /// <summary>
    /// Crea una nueva cuenta.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear la cuenta.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> CreateAsync(CreateCuentaCmd command);

    /// <summary>
    /// Actualiza una cuenta existente.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a actualizar.</param>
    /// <param name="command">Comando que contiene los datos actualizados de la cuenta.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateCuentaCmd command);

    /// <summary>
    /// Elimina una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a eliminar.</param>
    /// <returns>Un <see cref="HttpResponseMessage"/> que contiene la respuesta del servicio.</returns>
    Task<HttpResponseMessage> DeleteAsync(Guid id);
}
