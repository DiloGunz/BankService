using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Create;

public class CreateCuentaCmdHandler : IRequestHandler<CreateCuentaCmd, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCuentaCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCuentaCmd request, CancellationToken cancellationToken)
    {
        var existIdentificacion = await _unitOfWork.Cuentas.AnyAsync(x => x.NumeroCuenta == request.NumeroCuenta.Trim());

        if (existIdentificacion)
        {
            return Error.Validation("Cuenta.NumeroCuenta", "El número de cuenta ingresado ya existe.");
        }

        var cuenta = new Cuenta()
        {
            NumeroCuenta = request.NumeroCuenta,
            TipoCuenta = request.TipoCuenta,
            SaldoInicial = request.SaldoInicial,
            Estado = request.Estado,
            ClienteId = request.ClienteId,
        };

        await _unitOfWork.Cuentas.AddAsync(cuenta);
        await _unitOfWork.SaveChangesAsync();

        return cuenta.CuentaId;
    }
}