using Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.Commands;
using Api.Gateway.WebClient.Proxies.Config;
using Api.Gateway.WebClient.Proxies.Extensions;
using Microsoft.Extensions.Options;

namespace Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies.Implementation;

/// <summary>
/// Implementación del proxy para interactuar con el servicio de movimientos.
/// Proporciona métodos para realizar operaciones CRUD en el servicio de movimientos.
/// </summary>
public class MovimientoProxy : IMovimientoProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    // Ruta base para el recurso de movimientos
    private readonly string _movimientoRoute;

    /// <summary>
    /// Constructor de MovimientoProxy.
    /// </summary>
    /// <param name="apiUrls">Configuración que contiene las URL base para los servicios de la API.</param>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> para realizar solicitudes HTTP.</param>
    /// <exception cref="ArgumentNullException">Lanzada si apiUrls o httpClient son nulos.</exception>
    public MovimientoProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
    {
        _apiUrls = apiUrls.Value ?? throw new ArgumentNullException(nameof(apiUrls));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        // Agregar el token Bearer a las solicitudes HTTP
        _httpClient.AddBearerToken();

        // Construir la ruta base para el recurso de movimientos
        _movimientoRoute = $"{_apiUrls.AccountMgmt}/api/movimiento";
    }

    /// <summary>
    /// Crea un nuevo movimiento en el servicio.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear el movimiento.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> CreateAsync(CreateMovimientoCmd command)
    {
        var request = await _httpClient.ExecutePostAsync(_movimientoRoute, command);
        return request;
    }

    /// <summary>
    /// Crea un nuevo movimiento en el servicio utilizando el numero de cuenta y no ID.
    /// </summary>
    /// <param name="command">Comando que contiene los datos necesarios para crear el movimiento.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> CreateByNroCuentaAsync(CreateMovimientoByNroCuentaCmd command)
    {
        var request = await _httpClient.ExecutePostAsync($"{_movimientoRoute}/CreateByNroCuenta", command);
        return request;
    }

    /// <summary>
    /// Elimina un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a eliminar.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> DeleteAsync(Guid id)
    {
        var request = await _httpClient.ExecuteDeleteAsync($"{_movimientoRoute}/{id}");
        return request;
    }

    /// <summary>
    /// Obtiene todos los movimientos del servicio.
    /// </summary>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> GetAllAsync()
    {
        var request = await _httpClient.GetAsync(_movimientoRoute);
        return request;
    }

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
    {
        var request = await _httpClient.GetAsync($"{_movimientoRoute}/{id}");
        return request;
    }

    /// <summary>
    /// Actualiza un movimiento existente.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a actualizar.</param>
    /// <param name="command">Comando que contiene los datos actualizados del movimiento.</param>
    /// <returns>La respuesta HTTP del servicio.</returns>
    public async Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateMovimientoCmd command)
    {
        var request = await _httpClient.ExecutePutAsync($"{_movimientoRoute}/{id}", command);
        return request;
    }
}
