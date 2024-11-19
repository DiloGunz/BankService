namespace Api.Gateway.WebClient.Extensions;

/// <summary>
/// Extensiones para manejar respuestas de HttpResponseMessage.
/// Proporciona métodos utilitarios para deserializar contenido de respuestas HTTP.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Extensión para deserializar el contenido de un HttpResponseMessage a un tipo específico.
    /// </summary>
    /// <typeparam name="T">El tipo al que se desea deserializar el contenido.</typeparam>
    /// <param name="httpResponseMessage">La respuesta HTTP que contiene el contenido a deserializar.</param>
    /// <returns>El contenido deserializado al tipo especificado o un valor predeterminado si el contenido es nulo.</returns>
    /// <exception cref="InvalidOperationException">Se lanza si la deserialización falla o no se puede convertir el contenido al tipo especificado.</exception>
    public static async Task<T> DeserializerContentAsync<T>(this HttpResponseMessage httpResponseMessage)
    {
        // Deserializar el contenido del mensaje HTTP al tipo especificado
        return await httpResponseMessage.Content.ReadFromJsonAsync<T>() ?? default!;
    }
}
