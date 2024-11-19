using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetById;

public class GetByIdCuentaQueryValidator : AbstractValidator<GetByIdCuentaQuery>
{
    public GetByIdCuentaQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}