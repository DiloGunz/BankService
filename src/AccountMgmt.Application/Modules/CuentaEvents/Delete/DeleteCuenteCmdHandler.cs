using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Delete;

public class DeleteCuenteCmdHandler : IRequestHandler<DeleteCuentaCmd, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCuenteCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCuentaCmd request, CancellationToken cancellationToken)
    {
        var cliente = await _unitOfWork.Cuentas.GetByIdAsync(request.CuentaId);

        if (cliente is not Cuenta)
        {
            return Error.NotFound("Cuenta.NotFound", "No se encontró la cuenta con el Id proporcionado.");
        }

        _unitOfWork.Cuentas.Remove(cliente);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}