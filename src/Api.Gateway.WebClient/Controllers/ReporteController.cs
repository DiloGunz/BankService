using Api.Gateway.WebClient.Controllers.Generic;
using Api.Gateway.WebClient.Extensions;
using Api.Gateway.WebClient.Models.ClientMgmt.Cliente.DTOs;
using Api.Gateway.WebClient.Models.ReporteMgmt.DTOs;
using Api.Gateway.WebClient.Models.ReporteMgmt.Queries;
using Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies;
using Api.Gateway.WebClient.Proxies.Modules.ReportesProxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers;

/// <summary>
/// Controlador para gestionar reportes relacionados con los clientes.
/// Proporciona un endpoint para generar reportes de estado de cuenta por rango de fechas y cliente.
/// </summary>
[Route("api/[controller]")]
public class ReporteController : ApiController
{
    private readonly IClienteProxy _clienteProxy;
    private readonly IReporteProxy _reporteProxy;

    /// <summary>
    /// Constructor del ReporteController.
    /// </summary>
    /// <param name="clienteProxy">Proxy para interactuar con el servicio de clientes.</param>
    /// <param name="reporteProxy">Proxy para interactuar con el servicio de reportes.</param>
    /// <exception cref="ArgumentNullException">Se lanza si alguno de los proxies es nulo.</exception>
    public ReporteController(IClienteProxy clienteProxy, IReporteProxy reporteProxy)
    {
        _clienteProxy = clienteProxy ?? throw new ArgumentNullException(nameof(clienteProxy));
        _reporteProxy = reporteProxy ?? throw new ArgumentNullException(nameof(reporteProxy));
    }

    /// <summary>
    /// Genera un reporte de estado de cuenta para un cliente específico en un rango de fechas.
    /// </summary>
    /// <param name="fechaInicial">Fecha inicial del rango.</param>
    /// <param name="fechaFinal">Fecha final del rango.</param>
    /// <param name="clienteId">Identificador único del cliente.</param>
    /// <returns>Un reporte de estado de cuenta o un error si ocurre un problema.</returns>
    /// <response code="200">Reporte generado exitosamente.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet("estadocuenta")]
    public async Task<IActionResult> ByDatesAndClient(DateTimeOffset fechaInicial, DateTimeOffset fechaFinal, Guid clienteId)
    {
        // Obtener información del cliente por su ID
        var clienteResult = await _clienteProxy.GetByIdAsync(clienteId);
        if (!clienteResult.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(clienteResult);
        }

        var cliente = await clienteResult.DeserializerContentAsync<ClienteDto>();

        // Obtener el reporte de estado de cuenta basado en el rango de fechas y cliente
        var reporteResult = await _reporteProxy.GetEstadoCuentaAsync(new EstadoCuentaQuery(fechaInicial, fechaFinal, cliente.ClienteId));
        if (!reporteResult.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(reporteResult);
        }

        var estadoCuenta = await reporteResult.DeserializerContentAsync<List<EstadoCuentaDto>>();

        // Agregar información del cliente al reporte
        foreach (var item in estadoCuenta)
        {
            item.Cliente = cliente.Persona.Nombre;
        }

        return Ok(estadoCuenta);
    }
}
