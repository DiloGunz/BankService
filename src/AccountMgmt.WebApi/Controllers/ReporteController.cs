using AccountMgmt.Application.Modules.MovimientoEvents.EstadoCuenta;
using AccountMgmt.WebApi.Controllers.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgmt.WebApi.Controllers;

/// <summary>
/// Controlador para gestionar los reportes relacionados con las cuentas.
/// Proporciona un endpoint para obtener el estado de cuenta.
/// </summary>
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ReporteController : ApiController
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor del ReporteController.
    /// </summary>
    /// <param name="mediator">Instancia de ISender utilizada para manejar las consultas.</param>
    /// <exception cref="ArgumentNullException">Lanzada si el mediador es nulo.</exception>
    public ReporteController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Genera un reporte del estado de cuenta de un cliente.
    /// </summary>
    /// <param name="query">Consulta que contiene los parámetros necesarios para generar el estado de cuenta.</param>
    /// <returns>El reporte del estado de cuenta o errores en caso de fallar.</returns>
    /// <response code="200">Reporte generado exitosamente.</response>
    /// <response code="400">Datos inválidos en la solicitud.</response>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EstadoCuenta([FromQuery] EstadoCuentaQuery query)
    {
        var estadoCuentaResult = await _mediator.Send(query);

        return estadoCuentaResult.Match(
            reporte => Ok(reporte),
            errors => Problem(errors)
        );
    }
}