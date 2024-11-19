using AccountMgmt.Application.Modules.MovimientoEvents.Common;
using AccountMgmt.Domain.Interfaces;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.GetAll;

public class GetAllMovimientoQueryHandler : IRequestHandler<GetAllMovimientoQuery, ErrorOr<IReadOnlyList<MovimientoDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMovimientoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<IReadOnlyList<MovimientoDto>>> Handle(GetAllMovimientoQuery request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.Movimientos.GetAllAsNoTrackingAsync();
        return _mapper.Map<List<MovimientoDto>>(query);
    }
}