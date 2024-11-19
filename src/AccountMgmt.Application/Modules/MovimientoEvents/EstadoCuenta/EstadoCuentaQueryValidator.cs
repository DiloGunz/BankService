using FluentValidation;

namespace AccountMgmt.Application.Modules.MovimientoEvents.EstadoCuenta;

public class EstadoCuentaQueryValidator : AbstractValidator<EstadoCuentaQuery>
{
    public EstadoCuentaQueryValidator()
    {
        RuleFor(x => x.FechaInicio)
            .Must(x => x > DateTimeOffset.MinValue);

        RuleFor(x => x.FechaFin)
            .Must(x => x > DateTimeOffset.MinValue);

        RuleFor(x => x.ClienteId)
            .NotEmpty();

        RuleFor(x => x)
            .Must(model => model.FechaInicio <= model.FechaFin)
            .WithMessage("La fecha inicial no debe ser posterior a la fecha final");
    }
}