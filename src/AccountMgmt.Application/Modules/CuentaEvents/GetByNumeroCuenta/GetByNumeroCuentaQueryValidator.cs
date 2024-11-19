using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetByNumeroCuenta;

public class GetByNumeroCuentaQueryValidator : AbstractValidator<GetByNumeroCuentaQuery>
{
	public GetByNumeroCuentaQueryValidator()
	{
        RuleFor(x => x.NumeroCuenta).NotEmpty();
    }
}