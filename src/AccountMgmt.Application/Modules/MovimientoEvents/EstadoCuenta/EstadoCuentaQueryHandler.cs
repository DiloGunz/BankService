using AccountMgmt.Domain.Interfaces;
using AccountMgmt.Domain.Interfaces.Repositories;
using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountMgmt.Application.Modules.MovimientoEvents.EstadoCuenta;

public class EstadoCuentaQueryHandler : IRequestHandler<EstadoCuentaQuery, ErrorOr<IReadOnlyList<EstadoCuentaDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstadoCuentaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<IReadOnlyList<EstadoCuentaDto>>> Handle(EstadoCuentaQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Movimientos
            .GetAllAsNoTrackingAsync(x =>
                x.Fecha >= request.FechaInicio &&
                x.Fecha <= request.FechaFin &&
                x.Cuenta!.ClienteId == request.ClienteId,
                include: x => x.Include(y => y.Cuenta)!,
                orderBy: x => x.OrderByDescending(y => y.Fecha));

        return _mapper.Map<List<EstadoCuentaDto>>(data);
    }
}