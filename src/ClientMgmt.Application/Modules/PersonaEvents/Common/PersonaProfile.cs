using AutoMapper;
using ClientMgmt.Domain.Entities;

namespace ClientMgmt.Application.Modules.PersonaEvents.Common;

public class PersonaProfile : Profile
{
    public PersonaProfile()
    {
        CreateMap<Persona, PersonaDto>();
    }
}