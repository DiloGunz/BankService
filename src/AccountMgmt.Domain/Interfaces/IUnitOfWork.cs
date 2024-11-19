using AccountMgmt.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccountMgmt.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();

    ICuentaRepository Cuentas { get; }
    IMovimientoRepository Movimientos { get; }
}