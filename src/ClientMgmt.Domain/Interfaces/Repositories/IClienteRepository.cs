using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces.Repositories.Generic;

namespace ClientMgmt.Domain.Interfaces.Repositories;

public interface IClienteRepository :
    IReadRepository<Cliente>,
    ICreateRepository<Cliente>,
    IUpdateRepository<Cliente>,
    IRemoveRepository<Cliente>
{

}