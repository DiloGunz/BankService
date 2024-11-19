using FluentValidation;

namespace ClientMgmt.Application.Modules.PersonaEvents.GetById;

public class GetByIdPersonaQueryValidator : AbstractValidator<GetByIdPersonaQuery>
{
    public GetByIdPersonaQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}