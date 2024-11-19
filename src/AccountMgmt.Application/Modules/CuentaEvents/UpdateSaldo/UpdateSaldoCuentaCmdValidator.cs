using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.UpdateSaldo;

public class UpdateSaldoCuentaCmdValidator : AbstractValidator<UpdateSaldoCuentaCmd>
{
	public UpdateSaldoCuentaCmdValidator()
	{
		RuleFor(x => x.CuentaId)
			.NotEmpty();

		RuleFor(x => x.TipoMovimiento)
			.IsInEnum();

		RuleFor(x => x.Valor)
			.Must(x => x != 0);

		When(x => x.TipoMovimiento == Domain.Enums.GenericEnums.TipoMovimiento.Retiro, () =>
		{
			RuleFor(x => x.Valor)
			.Must(x => x < 0);
		});

        When(x => x.TipoMovimiento == Domain.Enums.GenericEnums.TipoMovimiento.Deposito, () =>
        {
            RuleFor(x => x.Valor)
			.Must(x => x > 0);
        });
    }
}