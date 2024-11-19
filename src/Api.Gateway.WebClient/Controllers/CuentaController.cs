using Api.Gateway.WebClient.Controllers.Generic;
using Api.Gateway.WebClient.Extensions;
using Api.Gateway.WebClient.Models.AccountMgmt.Cuenta.Commands;
using Api.Gateway.WebClient.Models.AccountMgmt.Cuenta.DTOs;
using Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con las cuentas.
/// Proporciona endpoints para CRUD (Crear, Leer, Actualizar, Eliminar) utilizando un cliente proxy para comunicarse con otra API o servicio.
/// </summary>
[Route("api/[controller]")]
public class CuentaController : ApiController
{
    private readonly ICuentaProxy _cuentaProxy;

    /// <summary>
    /// Constructor del CuentaController.
    /// </summary>
    /// <param name="cuentaProxy">Instancia del cliente proxy para comunicarse con el servicio de cuentas.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el cliente proxy es nulo.</exception>
    public CuentaController(ICuentaProxy cuentaProxy)
    {
        _cuentaProxy = cuentaProxy ?? throw new ArgumentNullException(nameof(cuentaProxy));
    }

    /// <summary>
    /// Obtiene una lista de todas las cuentas.
    /// </summary>
    /// <returns>Una lista de cuentas o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Lista de cuentas obtenida exitosamente.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _cuentaProxy.GetAllAsync();

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<List<CuentaDto>>());
    }

    /// <summary>
    /// Obtiene una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta.</param>
    /// <returns>Una cuenta específica o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cuenta obtenida exitosamente.</response>
    /// <response code="404">Cuenta no encontrada.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _cuentaProxy.GetByIdAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<CuentaDto>());
    }

    /// <summary>
    /// Crea una nueva cuenta.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear la cuenta.</param>
    /// <returns>El ID de la cuenta creada o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cuenta creada exitosamente.</response>
    /// <response code="400">Error de validación en los datos de la cuenta.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCuentaCmd command)
    {
        var result = await _cuentaProxy.CreateAsync(command);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<Guid>());
    }

    /// <summary>
    /// Actualiza una cuenta existente.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a actualizar.</param>
    /// <param name="command">Comando con los datos actualizados de la cuenta.</param>
    /// <returns>Un resultado de éxito o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cuenta actualizada exitosamente.</response>
    /// <response code="400">Error de validación en los datos proporcionados.</response>
    /// <response code="404">Cuenta no encontrada.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCuentaCmd command)
    {
        var result = await _cuentaProxy.UpdateAsync(id, command);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok();
    }

    /// <summary>
    /// Elimina una cuenta por su ID.
    /// </summary>
    /// <param name="id">Identificador único de la cuenta a eliminar.</param>
    /// <returns>Un resultado exitoso o un error si ocurre un problema en el proxy.</returns>
    /// <response code="204">Cuenta eliminada exitosamente.</response>
    /// <response code="404">Cuenta no encontrada.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _cuentaProxy.DeleteAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return NoContent();
    }
}
