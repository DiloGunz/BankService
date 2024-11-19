using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Delete;

public class DeleteMovimientoCmdHandler : IRequestHandler<DeleteMovimientoCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovimientoCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteMovimientoCmd request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Movimientos.GetByIdAsync(request.MovimientoId);

        if (persona is not Movimiento)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        _unitOfWork.Movimientos.Remove(persona);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}