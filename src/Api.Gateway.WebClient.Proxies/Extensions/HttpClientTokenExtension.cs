namespace Api.Gateway.WebClient.Proxies.Extensions;

/// <summary>
/// Extensión para agregar un token de autenticación Bearer a las solicitudes de <see cref="HttpClient"/>.
/// </summary>
public static class HttpClientTokenExtension
{
    /// <summary>
    /// Agrega un token de autenticación Bearer al encabezado de autorización del <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="client">Instancia de <see cref="HttpClient"/> a la que se agregará el token.</param>
    /// <remarks>
    /// Esta implementación está pendiente. Se debe completar la lógica para obtener y agregar el token.
    /// </remarks>
    public static void AddBearerToken(this HttpClient client)
    {
        // TODO: Implementar la lógica para obtener el token
        // Ejemplo:
        // string token = ObtenerToken(); // Método para obtener el token
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
