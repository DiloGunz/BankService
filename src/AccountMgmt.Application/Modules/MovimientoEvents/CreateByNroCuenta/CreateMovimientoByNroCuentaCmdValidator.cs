using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.CreateByNroCuenta;

public class CreateMovimientoByNroCuentaCmdValidator : AbstractValidator<CreateMovimientoByNroCuentaCmd>
{
    public CreateMovimientoByNroCuentaCmdValidator()
    {
        RuleFor(x => x.Valor)
            .Must(x => x != 0);

        RuleFor(x => x.NumeroCuenta)
            .NotEmpty();
    }
}