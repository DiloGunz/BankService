using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces.Repositories.Generic;

namespace AccountMgmt.Domain.Interfaces.Repositories;

public interface ICuentaRepository :
    IReadRepository<Cuenta>,
    ICreateRepository<Cuenta>,
    IUpdateRepository<Cuenta>,
    IRemoveRepository<Cuenta>
{

}