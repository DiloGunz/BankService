using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.Create;

public class CreateCuentaCmdValidator : AbstractValidator<CreateCuentaCmd>
{
    public CreateCuentaCmdValidator()
    {
        RuleFor(x => x.NumeroCuenta)
            .NotEmpty();

        RuleFor(x => x.TipoCuenta)
            .NotEmpty();

        RuleFor(x => x.SaldoInicial)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ClienteId)
            .NotEmpty();
    }
}