using FluentValidation;

namespace ClientMgmt.Application.Modules.PersonaEvents.Update;

/// <summary>
/// Validador para el comando UpdateClienteCmd.
/// Verifica que los datos proporcionados para actualizar un cliente sean válidos.
/// </summary>
public class UpdateClienteCmdValidator : AbstractValidator<UpdateClienteCmd>
{
    /// <summary>
    /// Constructor que define las reglas de validación para el comando UpdateClienteCmd.
    /// </summary>
    public UpdateClienteCmdValidator()
    {
        // Validar que el identificador del cliente no esté vacío
        RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("El identificador del cliente no puede estar vacío.");

        // Validar que los datos de la persona asociada sean válidos utilizando su validador específico
        RuleFor(x => x.Persona)
            .SetValidator(new UpdatePersonaCmdValidator());

        // Validar que la contraseña no sea vacía
        RuleFor(x => x.Contrasena)
            .NotEmpty()
            .WithMessage("La contraseña no puede estar vacía.");
    }
}
