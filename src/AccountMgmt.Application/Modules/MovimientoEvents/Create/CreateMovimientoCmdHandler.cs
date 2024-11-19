using AccountMgmt.Application.Modules.CuentaEvents.GetById;
using AccountMgmt.Application.Modules.CuentaEvents.UpdateSaldo;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Create;

public class CreateMovimientoCmdHandler : IRequestHandler<CreateMovimientoCmd, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediator;

    public CreateMovimientoCmdHandler(IUnitOfWork unitOfWork, ISender mediator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateMovimientoCmd request, CancellationToken cancellationToken)
    {
        var cuenta = await _mediator.Send(new GetByIdCuentaQuery(request.CuentaId));

        if (cuenta.IsError)
        {
            return cuenta.Errors;
        }

        using var trx = await _unitOfWork.BeginTransactionAsync();

        var movimiento = new Movimiento
        {
            TipoMovimiento = request.TipoMovimiento,
            Valor = request.Valor,
            Saldo = cuenta.Value.SaldoInicial,
            CuentaId = request.CuentaId
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