using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Update;

public class UpdateCuentaCmdHandler : IRequestHandler<UpdateCuentaCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCuentaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCuentaCmd request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Cuentas.GetByIdAsync(request.CuentaId);

        if (persona is not Cuenta)
        {
            return Error.NotFound("Cuenta.NotFound", "No se encontró la cuenta con el Id proporcionado.");
        }

        persona.NumeroCuenta = request.NumeroCuenta;
        persona.TipoCuenta = request.TipoCuenta;
        persona.SaldoInicial = request.SaldoInicial;
        persona.Estado = request.Estado;

        _unitOfWork.Cuentas.Update(persona);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
