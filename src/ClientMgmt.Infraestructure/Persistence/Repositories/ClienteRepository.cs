using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces.Repositories;

namespace ClientMgmt.Infraestructure.Persistence.Repositories;

public class ClienteRepository :
    GenericRepository<Cliente>,
    IClienteRepository
{
    public ClienteRepository(ClientMgmtContext context) : base(context)
    {
    }
}