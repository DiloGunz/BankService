using ClientMgmt.Application.Modules.PersonaEvents.Create;
using ClientMgmt.Application.Modules.PersonaEvents.Delete;
using ClientMgmt.Application.Modules.PersonaEvents.GetAll;
using ClientMgmt.Application.Modules.PersonaEvents.GetById;
using ClientMgmt.Application.Modules.PersonaEvents.Update;
using ClientMgmt.WebApi.Controllers.Generic;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientMgmt.WebApi.Controllers;

[Route("api/[controller]")]
public class PersonaController : ApiController
{
    private readonly ISender _mediator;

    public PersonaController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var personasResult = await _mediator.Send(new GetAllPersonaQuery());

        return personasResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var personaResult = await _mediator.Send(new GetByIdPersonaQuery(id));

        return personaResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaCmd command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaCmd command)
    {
        if (command.PersonaId != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Persona.UpdateInvalid", "El ID de la solicitud no coincide con el ID de la URL.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            personas => Ok(personas),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeletePersonaCmd(id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }
}