using AccountMgmt.Domain.Interfaces;
using AccountMgmt.Domain.Interfaces.Repositories;
using AccountMgmt.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccountMgmt.Infraestructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountMgmtContext _context;

    public UnitOfWork(AccountMgmtContext context)
    {
        _context = context;
        Cuentas = new CuentaRepository(_context);
        Movimientos = new MovimientoRepository(_context);
    }

    public ICuentaRepository Cuentas { get; }
    public IMovimientoRepository Movimientos { get; }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}