using AutoMapper;
using ClientMgmt.Application.Modules.PersonaEvents.Common;
using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace ClientMgmt.Application.Modules.PersonaEvents.GetById;

public record class GetByIdPersonaQueryHandler : IRequestHandler<GetByIdPersonaQuery, ErrorOr<PersonaDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdPersonaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<PersonaDto>> Handle(GetByIdPersonaQuery request, CancellationToken cancellationToken)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(request.Id);

        if (persona is not Persona)
        {
            return Error.NotFound("Persona.NotFound", "No se encontró la persona con el Id proporcionado.");
        }

        return _mapper.Map<PersonaDto>(persona);
    }
}