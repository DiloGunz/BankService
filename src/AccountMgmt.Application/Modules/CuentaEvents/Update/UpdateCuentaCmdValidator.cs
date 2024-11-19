using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.Update;

public class UpdateCuentaCmdValidator : AbstractValidator<UpdateCuentaCmd>
{
    public UpdateCuentaCmdValidator()
    {
        RuleFor(x => x.CuentaId)
            .NotEmpty();

        RuleFor(x => x.NumeroCuenta)
            .NotEmpty();

        RuleFor(x => x.TipoCuenta)
            .NotEmpty();

        RuleFor(x => x.SaldoInicial)
            .GreaterThanOrEqualTo(0);
    }
}