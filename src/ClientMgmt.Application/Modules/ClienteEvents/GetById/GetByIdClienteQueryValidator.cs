using FluentValidation;

namespace ClientMgmt.Application.Modules.ClienteEvents.GetById;

/// <summary>
/// Validador para la consulta GetByIdClienteQuery.
/// Verifica que los datos proporcionados para obtener un cliente por su ID sean válidos.
/// </summary>
public class GetByIdClienteQueryValidator : AbstractValidator<GetByIdClienteQuery>
{
    /// <summary>
    /// Constructor que define las reglas de validación para la consulta GetByIdClienteQuery.
    /// </summary>
    public GetByIdClienteQueryValidator()
    {
        // Validar que el identificador del cliente no esté vacío
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El identificador del cliente no puede estar vacío.");
    }
}
