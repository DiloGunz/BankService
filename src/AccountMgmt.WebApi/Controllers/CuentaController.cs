using AccountMgmt.Application.Modules.CuentaEvents.Create;
using AccountMgmt.Application.Modules.CuentaEvents.Delete;
using AccountMgmt.Application.Modules.CuentaEvents.GetAll;
using AccountMgmt.Application.Modules.CuentaEvents.GetById;
using AccountMgmt.Application.Modules.CuentaEvents.Update;
using AccountMgmt.WebApi.Controllers.Generic;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgmt.WebApi.Controllers;

/// <summary>
/// API para gestionar cuentas. Permite la creación, consulta, actualización y eliminación de cuentas.
/// </summary>
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CuentaController : ApiController
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor del controlador.
    /// </summary>
    /// <param name="mediator">Interfaz para enviar comandos y consultas.</param>
    public CuentaController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Obtiene todas las cuentas.
    /// </summary>
    /// <returns>Lista de cuentas disponibles.</returns>
    /// <response code="200">Si las cuentas se obtienen correctamente.</response>
    /// <response code="500">Si ocurre un error interno.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var cuentasResult = await _mediator.Send(new GetAllCuentaQuery());

        return cuentasResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Obtiene una cuenta por su ID.
    /// </summary>
    /// <param name="id">ID de la cuenta a obtener.</param>
    /// <returns>Detalles de la cuenta.</returns>
    /// <response code="200">Si se encuentra la cuenta.</response>
    /// <response code="404">Si la cuenta no se encuentra.</response>
    /// <response code="500">Si ocurre un error interno.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cuentaResult = await _mediator.Send(new GetByIdCuentaQuery(id));

        return cuentaResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Crea una nueva cuenta.
    /// </summary>
    /// <param name="command">Datos necesarios para crear la cuenta.</param>
    /// <returns>Cuenta creada.</returns>
    /// <response code="201">Si la cuenta se crea correctamente.</response>
    /// <response code="400">Si los datos enviados son inválidos.</response>
    /// <response code="500">Si ocurre un error interno.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCuentaCmd command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
                personas => CreatedAtAction(nameof(GetById), new { id = personas }, personas),
                errors => Problem(errors)
            );
    }

    /// <summary>
    /// Actualiza una cuenta existente.
    /// </summary>
    /// <param name="id">ID de la cuenta a actualizar.</param>
    /// <param name="command">Datos actualizados de la cuenta.</param>
    /// <returns>Cuenta actualizada.</returns>
    /// <response code="200">Si la cuenta se actualiza correctamente.</response>
    /// <response code="400">Si los datos enviados son inválidos.</response>
    /// <response code="500">Si ocurre un error interno.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCuentaCmd command)
    {
        if (command.CuentaId != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Cuenta.UpdateInvalid", "El ID de la solicitud no coincide con el ID de la URL.")
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
    /// Elimina una cuenta por su ID.
    /// </summary>
    /// <param name="id">ID de la cuenta a eliminar.</param>
    /// <returns>NoContent si la cuenta se elimina correctamente.</returns>
    /// <response code="204">Si la cuenta se elimina correctamente.</response>
    /// <response code="404">Si la cuenta no existe.</response>
    /// <response code="500">Si ocurre un error interno.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteCuentaCmd(id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }
}