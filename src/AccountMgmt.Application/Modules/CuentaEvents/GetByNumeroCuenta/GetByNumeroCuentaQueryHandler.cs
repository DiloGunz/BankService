using AccountMgmt.Application.Modules.CuentaEvents.Common;
using AccountMgmt.Domain.DomainErrors;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetByNumeroCuenta;

public class GetByNumeroCuentaQueryHandler : IRequestHandler<GetByNumeroCuentaQuery, ErrorOr<CuentaDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByNumeroCuentaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<CuentaDto>> Handle(GetByNumeroCuentaQuery request, CancellationToken cancellationToken)
    {
        var cuenta = await _unitOfWork
            .Cuentas
            .SingleOrDefaultAsNoTrackingAsync(x => x.NumeroCuenta == request.NumeroCuenta);

        if (cuenta is not Cuenta)
        {
            return CuentaErrors.NoEncontrado;
        }

        return _mapper.Map<CuentaDto>(cuenta);
    }
}