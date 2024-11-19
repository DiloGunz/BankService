using ClientMgmt.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ClientMgmt.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();


    IClienteRepository Clientes { get; }
    IPersonaRepository Personas { get; }
}