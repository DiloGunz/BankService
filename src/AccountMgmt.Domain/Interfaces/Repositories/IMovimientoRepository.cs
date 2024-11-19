using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces.Repositories.Generic;

namespace AccountMgmt.Domain.Interfaces.Repositories;

public interface IMovimientoRepository :
    IReadRepository<Movimiento>,
    ICreateRepository<Movimiento>,
    IUpdateRepository<Movimiento>,
    IRemoveRepository<Movimiento>
{
}