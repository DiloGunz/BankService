using System.Text;
using System.Web;

namespace Api.Gateway.WebClient.Proxies.Extensions;

/// <summary>
/// Extensiones para formatear URLs y agregar parámetros de consulta desde objetos.
/// </summary>
public static class UrlFormaterExtensions
{
    /// <summary>
    /// Agrega las propiedades de un objeto como parámetros de consulta a una URL base.
    /// </summary>
    /// <param name="urlBase">La URL base a la que se agregarán los parámetros de consulta.</param>
    /// <param name="obj">El objeto cuyas propiedades se agregarán como parámetros de consulta.</param>
    /// <returns>Una URL con los parámetros de consulta agregados.</returns>
    public static string AddObjectAsQueryString(this string urlBase, object obj)
    {
        // Obtener las propiedades del objeto
        var properties = obj.GetType().GetProperties();
        if (!properties.Any())
            return urlBase;

        // Construir la URL con parámetros de consulta
        var queryBuilder = new StringBuilder(urlBase);
        queryBuilder.Append('?');

        // Formatear cada propiedad como "clave=valor"
        var parameters = properties
            .Select(property =>
            {
                var key = HttpUtility.UrlEncode(property.Name);
                var value = HttpUtility.UrlEncode(property.GetValue(obj)?.ToString() ?? string.Empty);
                return $"{key}={value}";
            });

        // Agregar los parámetros a la URL
        queryBuilder.Append(string.Join("&", parameters));

        return queryBuilder.ToString();
    }
}
