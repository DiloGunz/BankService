using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces.Repositories;

namespace AccountMgmt.Infraestructure.Persistence.Repositories;

public class MovimientoRepository :
    GenericRepository<Movimiento>,
    IMovimientoRepository
{
    public MovimientoRepository(AccountMgmtContext context) : base(context)
    {
    }
}