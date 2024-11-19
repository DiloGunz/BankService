using AccountMgmt.Domain.DomainErrors;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Enums;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.UpdateSaldo;

public class UpdateSaldoCuentaCmdHandler : IRequestHandler<UpdateSaldoCuentaCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaldoCuentaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateSaldoCuentaCmd request, CancellationToken cancellationToken)
    {
        var cuenta = await _unitOfWork.Cuentas
                .FirstOrDefaultAsync(c => c.CuentaId == request.CuentaId);

        if (cuenta is not Cuenta)
        {
            return CuentaErrors.NoEncontrado;
        }

        if (request.TipoMovimiento == GenericEnums.TipoMovimiento.Retiro)
        {
            if (cuenta.SaldoInicial + request.Valor < 0)
            {
                return CuentaErrors.SaldoInsuficiente;
            }
        }

        cuenta.SaldoInicial += request.Valor;

        _unitOfWork.Cuentas.Update(cuenta);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}