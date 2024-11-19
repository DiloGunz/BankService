using AccountMgmt.Application.Modules.CuentaEvents.Common;
using AccountMgmt.Domain.Interfaces;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetAll;

public class GetAllCuentaQueryHandler : IRequestHandler<GetAllCuentaQuery, ErrorOr<IReadOnlyList<CuentaDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCuentaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<IReadOnlyList<CuentaDto>>> Handle(GetAllCuentaQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork
            .Cuentas
            .GetAllAsNoTrackingAsync();

        return _mapper.Map<List<CuentaDto>>(data);
    }
}