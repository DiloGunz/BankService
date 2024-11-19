using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.GetById;

public class GetByIdMovimientoQueryValidator : AbstractValidator<GetByIdMovimientoQuery>
{
    public GetByIdMovimientoQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}