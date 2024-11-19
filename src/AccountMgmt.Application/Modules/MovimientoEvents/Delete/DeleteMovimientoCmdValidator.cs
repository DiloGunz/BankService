using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Delete;

public class DeleteMovimientoCmdValidator : AbstractValidator<DeleteMovimientoCmd>
{
    public DeleteMovimientoCmdValidator()
    {
        RuleFor(x => x.MovimientoId).NotEmpty();
    }
}