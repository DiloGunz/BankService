using AccountMgmt.Application.Modules.CuentaEvents.GetByNumeroCuenta;
using AccountMgmt.Application.Modules.CuentaEvents.UpdateSaldo;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.CreateByNroCuenta;

public class CreateMovimientoByNroCuentaCmdHandler : IRequestHandler<CreateMovimientoByNroCuentaCmd, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediator;

    public CreateMovimientoByNroCuentaCmdHandler(IUnitOfWork unitOfWork, ISender mediator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateMovimientoByNroCuentaCmd request, CancellationToken cancellationToken)
    {
        var cuenta = await _mediator.Send(new GetByNumeroCuentaQuery(request.NumeroCuenta));

        if (cuenta.IsError)
        {
            return cuenta.Errors;
        }

        using var trx = await _unitOfWork.BeginTransactionAsync();

        var movimiento = new Movimiento
        {
            Valor = request.Valor,
            Saldo = cuenta.Value.SaldoInicial,
            CuentaId = cuenta.Value.CuentaId,
            TipoMovimiento = request.Valor >= 0 ? 
                    Domain.Enums.GenericEnums.TipoMovimiento.Deposito : 
                    Domain.Enums.GenericEnums.TipoMovimiento.Retiro 
            
        };

        await _unitOfWork.Movimientos.AddAsync(movimiento);
        await _unitOfWork.SaveChangesAsync();

        var updateSaldoCmd = new UpdateSaldoCuentaCmd()
        {
            CuentaId = cuenta.Value.CuentaId,
            TipoMovimiento = movimiento.TipoMovimiento,
            Valor = movimiento.Valor
        };

        var updateSaldoResponse = await _mediator.Send(updateSaldoCmd);

        if (updateSaldoResponse.IsError)
        {
            return updateSaldoResponse.Errors;
        }

        await trx.CommitAsync();

        return movimiento.MovimientoId;
    }
}

