using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetSaldo;

public class GetSaldoCuentaQueryHandler : IRequestHandler<GetSaldoCuentaQuery, ErrorOr<GetSaldoCuentaResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSaldoCuentaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetSaldoCuentaResponse>> Handle(GetSaldoCuentaQuery request, CancellationToken cancellationToken)
    {
        var cuenta = await _unitOfWork.Cuentas.GetByIdAsync(request.CuentaId);
        if (cuenta is not Cuenta)
        {
            return Error.NotFound("Cuenta.NotFound", "La cuenta no existe.");
        }

        if (!cuenta.Estado)
        {
            return Error.Validation("Cuenta.Estado", "La cuenta está desactivada.");
        }

        return new GetSaldoCuentaResponse()
        {
            Estado = cuenta.Estado,
            Saldo = cuenta.SaldoInicial
        };
    }
}