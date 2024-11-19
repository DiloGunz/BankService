using System.Text.Json;
using System.Text;

namespace Api.Gateway.WebClient.Proxies.Extensions;

/// <summary>
/// Extensiones para simplificar las operaciones HTTP con <see cref="HttpClient"/>.
/// Proporciona métodos utilitarios para realizar solicitudes GET, POST, PUT y DELETE.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Realiza una solicitud HTTP GET al URL especificado.
    /// </summary>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> utilizada para la solicitud.</param>
    /// <param name="url">La URL a la que se envía la solicitud GET.</param>
    /// <returns>La respuesta HTTP de la solicitud.</returns>
    public static async Task<HttpResponseMessage> ExecuteGetAsync(this HttpClient httpClient, string url)
    {
        var result = await httpClient.GetAsync(url);
        return result;
    }

    /// <summary>
    /// Realiza una solicitud HTTP POST al URL especificado con un cuerpo JSON.
    /// </summary>
    /// <typeparam name="TRequest">El tipo del objeto que se envía en el cuerpo de la solicitud.</typeparam>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> utilizada para la solicitud.</param>
    /// <param name="url">La URL a la que se envía la solicitud POST.</param>
    /// <param name="request">El objeto que se envía en el cuerpo de la solicitud.</param>
    /// <returns>La respuesta HTTP de la solicitud.</returns>
    public static async Task<HttpResponseMessage> ExecutePostAsync<TRequest>(this HttpClient httpClient, string url, TRequest request)
    {
        var result = await httpClient.PostAsync(url, GetStringContent(request));
        return result;
    }

    /// <summary>
    /// Realiza una solicitud HTTP PUT al URL especificado con un cuerpo JSON.
    /// </summary>
    /// <typeparam name="TRequest">El tipo del objeto que se envía en el cuerpo de la solicitud.</typeparam>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> utilizada para la solicitud.</param>
    /// <param name="url">La URL a la que se envía la solicitud PUT.</param>
    /// <param name="request">El objeto que se envía en el cuerpo de la solicitud.</param>
    /// <returns>La respuesta HTTP de la solicitud.</returns>
    public static async Task<HttpResponseMessage> ExecutePutAsync<TRequest>(this HttpClient httpClient, string url, TRequest request)
    {
        var result = await httpClient.PutAsync(url, GetStringContent(request));
        return result;
    }

    /// <summary>
    /// Realiza una solicitud HTTP DELETE al URL especificado.
    /// </summary>
    /// <param name="httpClient">Instancia de <see cref="HttpClient"/> utilizada para la solicitud.</param>
    /// <param name="url">La URL a la que se envía la solicitud DELETE.</param>
    /// <returns>La respuesta HTTP de la solicitud.</returns>
    public static async Task<HttpResponseMessage> ExecuteDeleteAsync(this HttpClient httpClient, string url)
    {
        var result = await httpClient.DeleteAsync(url);
        return result;
    }

    /// <summary>
    /// Convierte un objeto en contenido JSON para enviarlo en solicitudes HTTP.
    /// </summary>
    /// <typeparam name="TRequest">El tipo del objeto a convertir.</typeparam>
    /// <param name="request">El objeto que se serializa a JSON.</param>
    /// <returns>Una instancia de <see cref="StringContent"/> que contiene el JSON serializado.</returns>
    private static StringContent GetStringContent<TRequest>(TRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        return content;
    }
}

