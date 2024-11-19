using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces.Repositories;

namespace ClientMgmt.Infraestructure.Persistence.Repositories;

public class PersonaRepository :
    GenericRepository<Persona>,
    IPersonaRepository
{
    public PersonaRepository(ClientMgmtContext context) : base(context)
    {
    }
}