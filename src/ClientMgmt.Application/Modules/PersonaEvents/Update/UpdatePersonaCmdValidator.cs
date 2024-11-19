using FluentValidation;

namespace ClientMgmt.Application.Modules.PersonaEvents.Update;

public class UpdatePersonaCmdValidator : AbstractValidator<UpdatePersonaCmd>
{
	public UpdatePersonaCmdValidator()
	{
        RuleFor(x => x.PersonaId)
            .NotEmpty();

        RuleFor(x => x.Nombre)
            .NotEmpty();

        RuleFor(x => x.Genero)
            .NotEmpty();

        RuleFor(x => x.Edad)
            .GreaterThanOrEqualTo(18);

        RuleFor(x => x.Identificacion)
            .NotEmpty();

        RuleFor(x => x.Direccion)
            .NotEmpty();

        RuleFor(x => x.Telefono)
            .NotEmpty();
    }
}