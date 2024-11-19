using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Create;

public class CreateMovimientoCmdValidator : AbstractValidator<CreateMovimientoCmd>
{
    public CreateMovimientoCmdValidator()
    {
        RuleFor(x => x.TipoMovimiento)
            .IsInEnum();

        RuleFor(x => x.Valor)
            .Must(x => x != 0);

        RuleFor(x => x.CuentaId)
            .NotEmpty();
    }
}
