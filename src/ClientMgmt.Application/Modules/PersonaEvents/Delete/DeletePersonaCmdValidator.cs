using FluentValidation;

namespace ClientMgmt.Application.Modules.PersonaEvents.Delete;

public class DeletePersonaCmdValidator : AbstractValidator<DeletePersonaCmd>
{
	public DeletePersonaCmdValidator()
	{
		RuleFor(x => x.PersonaId).NotEmpty();
	}
}