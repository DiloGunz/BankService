using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces.Repositories;

namespace AccountMgmt.Infraestructure.Persistence.Repositories;

public class CuentaRepository :
    GenericRepository<Cuenta>,
    ICuentaRepository
{
    public CuentaRepository(AccountMgmtContext context) : base(context)
    {
    }
}