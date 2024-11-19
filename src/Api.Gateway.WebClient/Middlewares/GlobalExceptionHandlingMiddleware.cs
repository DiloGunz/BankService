using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Api.Gateway.WebClient.Middlewares;

/// <summary>
/// Middleware global para manejar excepciones no controladas en la aplicación.
/// Captura errores durante el procesamiento de solicitudes y devuelve una respuesta estructurada.
/// </summary>
public class GloblalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GloblalExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Constructor del middleware de manejo global de excepciones.
    /// </summary>
    /// <param name="logger">Instancia de ILogger para registrar los errores ocurridos.</param>
    public GloblalExceptionHandlingMiddleware(ILogger<GloblalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Lógica principal del middleware que captura excepciones no controladas.
    /// </summary>
    /// <param name="context">Contexto de la solicitud HTTP actual.</param>
    /// <param name="next">Delegate que invoca el siguiente middleware en la canalización.</param>
    /// <returns>Tarea asíncrona que representa la operación de middleware.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // Continuar con el siguiente middleware
            await next(context);
        }
        catch (Exception ex)
        {
            // Registrar el error
            _logger.LogError(ex, ex.Message);

            // Configurar la respuesta HTTP con un código 500 (Error Interno del Servidor)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Crear un objeto ProblemDetails para estructurar la respuesta
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error has occurred."
            };

            // Serializar el objeto ProblemDetails a formato JSON
            string json = JsonSerializer.Serialize(problem);

            // Configurar el tipo de contenido de la respuesta
            context.Response.ContentType = "application/json";

            // Escribir el contenido JSON en la respuesta
            await context.Response.WriteAsync(json);
        }
    }
}
