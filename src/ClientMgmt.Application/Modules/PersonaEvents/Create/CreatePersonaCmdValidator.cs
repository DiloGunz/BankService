using FluentValidation;

namespace ClientMgmt.Application.Modules.PersonaEvents.Create;

public class CreatePersonaCmdValidator : AbstractValidator<CreatePersonaCmd>
{
	public CreatePersonaCmdValidator()
	{
		RuleFor(x => x.Nombre)
			.NotEmpty();

		RuleFor(x=>x.Genero)
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
