using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces.Repositories.Generic;

namespace ClientMgmt.Domain.Interfaces.Repositories;

public interface IPersonaRepository :
    IReadRepository<Persona>,
    ICreateRepository<Persona>,
    IUpdateRepository<Persona>,
    IRemoveRepository<Persona>
{
}