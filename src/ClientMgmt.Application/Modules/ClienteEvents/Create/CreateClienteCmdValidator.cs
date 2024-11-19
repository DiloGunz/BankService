using ClientMgmt.Application.Modules.PersonaEvents.Create;
using FluentValidation;

namespace ClientMgmt.Application.Modules.ClienteEvents.Create;

/// <summary>
/// Validador para el comando CreateClienteCmd.
/// Verifica que los datos proporcionados para crear un cliente sean válidos.
/// </summary>
public class CreateClienteCmdValidator : AbstractValidator<CreateClienteCmd>
{
    /// <summary>
    /// Constructor que define las reglas de validación para el comando CreateClienteCmd.
    /// </summary>
    public CreateClienteCmdValidator()
    {
        // Validar las propiedades del comando Persona anidado utilizando su validador específico.
        RuleFor(x => x.Persona).SetValidator(new CreatePersonaCmdValidator());

        // Validar que la contraseña no sea vacía.
        RuleFor(x => x.Contrasena)
            .NotEmpty()
            .WithMessage("La contraseña no puede estar vacía.");
    }
}
