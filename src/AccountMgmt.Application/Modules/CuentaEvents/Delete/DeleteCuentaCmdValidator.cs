using FluentValidation;

namespace AccountMgmt.Application.Modules.CuentaEvents.Delete;

public class DeleteCuentaCmdValidator : AbstractValidator<DeleteCuentaCmd>
{
    public DeleteCuentaCmdValidator()
    {
        RuleFor(x => x.CuentaId).NotEmpty();
    }
}