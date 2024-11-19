using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Update;

public class UpdateMovimientoCmdValidator : AbstractValidator<UpdateMovimientoCmd>
{
    public UpdateMovimientoCmdValidator()
    {
        RuleFor(x => x.MovimientoId)
            .NotEmpty();

        RuleFor(x => x.TipoMovimiento)
            .IsInEnum();

        RuleFor(x => x.Valor)
            .Must(x => x != 0);
    }
}