using Api.Gateway.WebClient.Controllers.Generic;
using Api.Gateway.WebClient.Extensions;
using Api.Gateway.WebClient.Models.ClientMgmt.Cliente.Commands;
using Api.Gateway.WebClient.Models.ClientMgmt.Cliente.DTOs;
using Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los clientes mediante un proxy.
/// Proporciona endpoints para CRUD (Crear, Leer, Actualizar, Eliminar) utilizando un cliente proxy para comunicarse con otra API o servicio.
/// </summary>
[Route("api/[controller]")]
public class ClienteController : ApiController
{
    private readonly IClienteProxy _clienteProxy;

    /// <summary>
    /// Constructor del ClienteController.
    /// </summary>
    /// <param name="clienteProxy">Instancia del cliente proxy para comunicarse con el servicio de clientes.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el cliente proxy es nulo.</exception>
    public ClienteController(IClienteProxy clienteProxy)
    {
        _clienteProxy = clienteProxy ?? throw new ArgumentNullException(nameof(clienteProxy));
    }

    /// <summary>
    /// Constructor del ClienteController.
    /// </summary>
    /// <param name="clienteProxy">Instancia del cliente proxy para comunicarse con el servicio de clientes.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el cliente proxy es nulo.</exception>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _clienteProxy.GetAllAsync();
        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<List<ClienteDto>>());
    }

    /// <summary>
    /// Obtiene una lista de todos los clientes.
    /// </summary>
    /// <returns>Una lista de clientes o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Lista de clientes obtenida exitosamente.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _clienteProxy.GetByIdAsync(id);
        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<ClienteDto>());
    }

    /// <summary>
    /// Obtiene un cliente por su ID.
    /// </summary>
    /// <param name="id">Identificador único del cliente.</param>
    /// <returns>Un cliente específico o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cliente obtenido exitosamente.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteCmd command)
    {
        var result = await _clienteProxy.CreateAsync(command);
        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok(await result.DeserializerContentAsync<Guid>());
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear el cliente.</param>
    /// <returns>El ID del cliente creado o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cliente creado exitosamente.</response>
    /// <response code="400">Error de validación en los datos del cliente.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteCmd command)
    {
        var result = await _clienteProxy.UpdateAsync(id, command);
        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return Ok();
    }

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="id">Identificador único del cliente a actualizar.</param>
    /// <param name="command">Comando con los datos actualizados del cliente.</param>
    /// <returns>Un resultado de éxito o un error si ocurre un problema en el proxy.</returns>
    /// <response code="200">Cliente actualizado exitosamente.</response>
    /// <response code="400">Error de validación en los datos proporcionados.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error en la comunicación con el servicio.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _clienteProxy.DeleteAsync(id);
        if (!result.IsSuccessStatusCode)
        {
            return await BuildErrorResponse(result);
        }

        return NoContent();
    }
}
