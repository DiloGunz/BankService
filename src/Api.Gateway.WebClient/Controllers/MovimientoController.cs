using Api.Gateway.WebClient.Controllers.Generic;
using Api.Gateway.WebClient.Extensions;
using Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.Commands;
using Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.DTOs;
using Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los movimientos.
/// Proporciona endpoints para CRUD (Crear, Leer, Actualizar, Eliminar) utilizando un cliente proxy para comunicarse con otra API o servicio.
/// </summary>
[Route("api/[controller]")]
public class MovimientoController : ApiController
{
    private readonly IMovimientoProxy _movimientoProxy;

    /// <summary>
    /// Constructor del MovimientoController.
    /// </summary>
    /// <param name="movimientoProxy">Instancia del cliente proxy para comunicarse con el servicio de movimientos.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el cliente proxy es nulo.</exception>
    public MovimientoController(IMovimientoProxy movimientoProxy)
    {
        _movimientoProxy = movimientoProxy ?? throw new ArgumentNullException(nameof(movimientoProxy));
    }

    /// <summary>
    /// Obtiene una lista de todos los movimientos.
    /// </summary>
    /// <returns>Una lista de movimientos o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Lista de movimientos obtenida exitosamente.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _movimientoProxy.GetAllAsync();

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<List<MovimientoDto>>());
    }

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento.</param>
    /// <returns>Un movimiento específico o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Movimiento obtenido exitosamente.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _movimientoProxy.GetByIdAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<MovimientoDto>());
    }

    /// <summary>
    /// Crea un nuevo movimiento.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear el movimiento.</param>
    /// <returns>El ID del movimiento creado o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Movimiento creado exitosamente.</response>
    /// <response code="400">Error de validación en los datos del movimiento.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovimientoCmd command)
    {
        var result = await _movimientoProxy.CreateAsync(command);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<Guid>());
    }

    /// <summary>
    /// Crea un nuevo movimiento.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear el movimiento.</param>
    /// <returns>El ID del movimiento creado o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Movimiento creado exitosamente.</response>
    /// <response code="400">Error de validación en los datos del movimiento.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateByNroCuenta([FromBody] CreateMovimientoByNroCuentaCmd command)
    {
        var result = await _movimientoProxy.CreateByNroCuentaAsync(command);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<Guid>());
    }

    /// <summary>
    /// Actualiza un movimiento existente.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a actualizar.</param>
    /// <param name="command">Comando con los datos actualizados del movimiento.</param>
    /// <returns>Un resultado de éxito o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Movimiento actualizado exitosamente.</response>
    /// <response code="400">Error de validación en los datos proporcionados.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovimientoCmd command)
    {
        var result = await _movimientoProxy.UpdateAsync(id, command);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok();
    }

    /// <summary>
    /// Elimina un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a eliminar.</param>
    /// <returns>Un resultado exitoso o un error si ocurre un problema en el proxy.</returns>
    /// <response code="204">Movimiento eliminado exitosamente.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _movimientoProxy.DeleteAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return NoContent();
    }
}
