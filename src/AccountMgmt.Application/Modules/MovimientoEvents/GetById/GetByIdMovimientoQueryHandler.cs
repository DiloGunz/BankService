using AccountMgmt.Application.Modules.MovimientoEvents.Common;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.MovimientoEvents.GetById;

public record class GetByIdMovimientoQueryHandler : IRequestHandler<GetByIdMovimientoQuery, ErrorOr<MovimientoDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdMovimientoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<MovimientoDto>> Handle(GetByIdMovimientoQuery request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Movimientos.GetByIdAsync(request.Id);

        if (persona is not Movimiento)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró el movimiento con el Id proporcionado.");
        }

        return _mapper.Map<MovimientoDto>(persona);
    }
}