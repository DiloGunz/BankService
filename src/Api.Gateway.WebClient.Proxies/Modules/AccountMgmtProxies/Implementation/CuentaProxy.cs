using Api.Gateway.WebClient.Models.AccountMgmt.Cuenta.Commands;
using Api.Gateway.WebClient.Proxies.Config;
using Api.Gateway.WebClient.Proxies.Extensions;
using Microsoft.Extensions.Options;

namespace Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies.Implementation;

/// <summary>
/// Implementación del proxy para interactuar con el servicio de cuentas.
/// Proporciona métodos para realizar operaciones CRUD en el servicio de cuentas.
/// </summary>
public class CuentaProxy : ICuentaProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    // Ruta base para el recurso de cuentas
    private readonly string _cuentaRoute;

    /// <summary>
    /// Constructor de CuentaProxy.
    /// </summary>
    /// <param name="apiUrls">Configuración que contiene las URL base para los servicios de la API.</param>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> para realizar solicitudes HTTP.</param>
    /// <exception cref="ArgumentNullException">Lanzada si apiUrls o httpClient son nulos.</exception>
    public CuentaProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
    {
        _apiUrls = apiUrls.Value ?? throw new ArgumentNullException(nameof(apiUrls));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        // Agregar el token Bearer a las solicitudes HTTP
        _httpClient.AddBearerToken();

        // Construir la ruta base para el recurso de cuentas
        _cuentaRoute = $"{_apiUrls.AccountMgmt}/api/cuenta";
    }

    /// <summary>
    /// Crea una nueva cuenta en el servicio.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear la cuenta.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> CreateAsync(CreateCuentaCmd command)
    {
        var request = await _httpClient.ExecutePostAsync(_cuentaRoute, command);
        return request;
    }

    /// <summary>
    /// Elimina una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a eliminar.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> DeleteAsync(Guid id)
    {
        var request = await _httpClient.ExecuteDeleteAsync($"{_cuentaRoute}/{id}");
        return request;
    }

    /// <summary>
    /// Obtiene todas las cuentas del servicio.
    /// </summary>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> GetAllAsync()
    {
        var request = await _httpClient.GetAsync(_cuentaRoute);
        return request;
    }

    /// <summary>
    /// Obtiene una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
    {
        var request = await _httpClient.GetAsync($"{_cuentaRoute}/{id}");
        return request;
    }

    /// <summary>
    /// Actualiza una cuenta existente.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a actualizar.</param>
    /// <param name="command">Comando que contiene los datos actualizados de la cuenta.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateCuentaCmd command)
    {
        var request = await _httpClient.ExecutePutAsync($"{_cuentaRoute}/{id}", command);
        return request;
    }
}
