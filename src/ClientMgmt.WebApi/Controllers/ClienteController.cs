using ClientMgmt.Application.Modules.ClienteEvents.Create;
using ClientMgmt.Application.Modules.ClienteEvents.Delete;
using ClientMgmt.Application.Modules.ClienteEvents.GetAll;
using ClientMgmt.Application.Modules.ClienteEvents.GetById;
using ClientMgmt.Application.Modules.PersonaEvents.Update;
using ClientMgmt.WebApi.Controllers.Generic;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientMgmt.WebApi.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los clientes.
/// Proporciona endpoints para CRUD (Crear, Leer, Actualizar, Eliminar).
/// </summary>
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ClienteController : ApiController
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor del ClienteController.
    /// </summary>
    /// <param name="mediator">Instancia de ISender utilizada para manejar los comandos y consultas.</param>
    /// <exception cref="ArgumentNullException">Lanzada si el mediador es nulo.</exception>
    public ClienteController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Obtiene una lista de todos los clientes registrados.
    /// </summary>
    /// <returns>Una lista de clientes o errores en caso de fallar.</returns>
    /// <response code="200">Lista de clientes obtenida exitosamente.</response>
    /// <response code="500">Error al procesar la solicitud.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var personasResult = await _mediator.Send(new GetAllClienteQuery());

        return personasResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Obtiene un cliente por su ID.
    /// </summary>
    /// <param name="id">Identificador único del cliente.</param>
    /// <returns>El cliente correspondiente o errores en caso de fallar.</returns>
    /// <response code="200">Cliente encontrado exitosamente.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error al procesar la solicitud.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var clienteResult = await _mediator.Send(new GetByIdClienteQuery(id));

        return clienteResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="command">Comando con los datos del cliente a crear.</param>
    /// <returns>El cliente creado o errores en caso de fallar.</returns>
    /// <response code="201">Cliente creado exitosamente.</response>
    /// <response code="400">Datos inválidos en la solicitud.</response>
    /// <response code="500">Error al procesar la solicitud.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateClienteCmd command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="id">Identificador único del cliente a actualizar.</param>
    /// <param name="command">Comando con los datos actualizados del cliente.</param>
    /// <returns>El cliente actualizado o errores en caso de fallar.</returns>
    /// <response code="200">Cliente actualizado exitosamente.</response>
    /// <response code="400">Datos inválidos o IDs no coinciden.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error al procesar la solicitud.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteCmd command)
    {
        if (command.ClienteId != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Cliente.UpdateInvalid", "El ID de la solicitud no coincide con el ID de la URL.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Elimina un cliente por su ID.
    /// </summary>
    /// <param name="id">Identificador único del cliente a eliminar.</param>
    /// <returns>Respuesta de eliminación o errores en caso de fallar.</returns>
    /// <response code="204">Cliente eliminado exitosamente.</response>
    /// <response code="404">Cliente no encontrado.</response>
    /// <response code="500">Error al procesar la solicitud.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteClienteCmd(id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }
}