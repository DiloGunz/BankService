using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Update;

public class UpdateMovimientoCmdHandler : IRequestHandler<UpdateMovimientoCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovimientoCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateMovimientoCmd request, CancellationToken cancellationToken)
    {
        var Movimiento = await _unitOfWork.Movimientos.GetByIdAsync(request.MovimientoId);

        if (Movimiento is not Domain.Entities.Movimiento)
        {
            return Error.NotFound("Movimiento.NotFound", "No se encontró el movimiento con el Id proporcionado.");
        }

        Movimiento.TipoMovimiento = request.TipoMovimiento;
        Movimiento.Valor = request.Valor;

        _unitOfWork.Movimientos.Update(Movimiento);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
