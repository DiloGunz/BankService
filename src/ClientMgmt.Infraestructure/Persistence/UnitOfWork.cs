using ClientMgmt.Domain.Interfaces;
using ClientMgmt.Domain.Interfaces.Repositories;
using ClientMgmt.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ClientMgmt.Infraestructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClientMgmtContext _context;

    public UnitOfWork(ClientMgmtContext context)
    {
        _context = context;
        Clientes = new ClienteRepository(_context);
        Personas = new PersonaRepository(_context);
    }

    public IClienteRepository Clientes { get; }
    public IPersonaRepository Personas { get; }

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