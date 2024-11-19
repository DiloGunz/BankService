using FluentValidation;

namespace ClientMgmt.Application.Modules.ClienteEvents.Delete;

/// <summary>
/// Validador para el comando DeleteClienteCmd.
/// Verifica que los datos proporcionados para eliminar un cliente sean válidos.
/// </summary>
public class DeleteClienteCmdValidator : AbstractValidator<DeleteClienteCmd>
{
    /// <summary>
    /// Constructor que define las reglas de validación para el comando DeleteClienteCmd.
    /// </summary>
    public DeleteClienteCmdValidator()
    {
        // Validar que el identificador del cliente no esté vacío
        RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("El identificador del cliente no puede estar vacío.");
    }
}
