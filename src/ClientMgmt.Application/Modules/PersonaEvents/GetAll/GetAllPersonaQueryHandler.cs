using AutoMapper;
using ClientMgmt.Application.Modules.PersonaEvents.Common;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.GetAll;

public class GetAllPersonaQueryHandler : IRequestHandler<GetAllPersonaQuery, ErrorOr<IReadOnlyList<PersonaDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPersonaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<IReadOnlyList<PersonaDto>>> Handle(GetAllPersonaQuery request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.Personas.GetAllAsNoTrackingAsync();
        return _mapper.Map<List<PersonaDto>>(query);
    }
}