using AccountMgmt.Application.Modules.MovimientoEvents.Create;
using AccountMgmt.Application.Modules.MovimientoEvents.CreateByNroCuenta;
using AccountMgmt.Application.Modules.MovimientoEvents.Delete;
using AccountMgmt.Application.Modules.MovimientoEvents.GetAll;
using AccountMgmt.Application.Modules.MovimientoEvents.GetById;
using AccountMgmt.Application.Modules.MovimientoEvents.Update;
using AccountMgmt.WebApi.Controllers.Generic;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgmt.WebApi.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los movimientos.
/// Proporciona endpoints para CRUD (Crear, Leer, Actualizar, Eliminar).
/// </summary>
[Route("api/[controller]")]
public class MovimientoController : ApiController
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor del MovimientoController.
    /// </summary>
    /// <param name="mediator">Instancia de ISender utilizada para manejar los comandos y consultas.</param>
    /// <exception cref="ArgumentNullException">Lanzada si el mediador es nulo.</exception>
    public MovimientoController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Obtiene una lista de todos los movimientos registrados.
    /// </summary>
    /// <returns>Una lista de movimientos o errores en caso de fallar.</returns>
    /// <response code="200">Lista de movimientos obtenida exitosamente.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var movimientosResult = await _mediator.Send(new GetAllMovimientoQuery());

        return movimientosResult.Match(
            movimientos => Ok(movimientos),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Obtiene un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento.</param>
    /// <returns>El movimiento correspondiente o errores en caso de fallar.</returns>
    /// <response code="200">Movimiento encontrado exitosamente.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var movimientoResult = await _mediator.Send(new GetByIdMovimientoQuery(id));

        return movimientoResult.Match(
            movimiento => Ok(movimiento),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Crea un nuevo movimiento.
    /// </summary>
    /// <param name="command">Comando con los datos del movimiento a crear.</param>
    /// <returns>El movimiento creado o errores en caso de fallar.</returns>
    /// <response code="201">Movimiento creado exitosamente.</response>
    /// <response code="400">Datos inválidos en la solicitud.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMovimientoCmd command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            movimiento => CreatedAtAction(nameof(GetById), new { id = movimiento }, movimiento),
            errors => Problem(errors)
        );
    }

    // <summary>
    /// Crea un nuevo movimiento utilizando el número de cuenta y no el ID.
    /// </summary>
    /// <param name="command">Comando con los datos del movimiento a crear.</param>
    /// <returns>El movimiento creado o errores en caso de fallar.</returns>
    /// <response code="201">Movimiento creado exitosamente.</response>
    /// <response code="400">Datos inválidos en la solicitud.</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateByNroCuenta([FromBody] CreateMovimientoByNroCuentaCmd command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            movimiento => CreatedAtAction(nameof(GetById), new { id = movimiento }, movimiento),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Actualiza un movimiento existente.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a actualizar.</param>
    /// <param name="command">Comando con los datos actualizados del movimiento.</param>
    /// <returns>El movimiento actualizado o errores en caso de fallar.</returns>
    /// <response code="200">Movimiento actualizado exitosamente.</response>
    /// <response code="400">Datos inválidos o IDs no coinciden.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovimientoCmd command)
    {
        if (command.MovimientoId != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Movimiento.UpdateInvalid", "El ID de la solicitud no coincide con el ID de la URL.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            movimiento => Ok(movimiento),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Elimina un movimiento por su ID.
    /// </summary>
    /// <param name="id">Identificador único del movimiento a eliminar.</param>
    /// <returns>Respuesta de eliminación o errores en caso de fallar.</returns>
    /// <response code="204">Movimiento eliminado exitosamente.</response>
    /// <response code="404">Movimiento no encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteMovimientoCmd(id));

        return deleteResult.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}