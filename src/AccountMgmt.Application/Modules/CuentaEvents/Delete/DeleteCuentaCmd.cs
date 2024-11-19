using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.Delete;

public record DeleteCuentaCmd : IRequest<ErrorOr<Unit>>
{
    public DeleteCuentaCmd(Guid id)
    {
        CuentaId = id;
    }
    public Guid CuentaId { get; set; }
}