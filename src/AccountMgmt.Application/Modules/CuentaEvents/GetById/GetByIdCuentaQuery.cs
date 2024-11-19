using AccountMgmt.Application.Modules.CuentaEvents.Common;
using ErrorOr;
using MediatR;

namespace AccountMgmt.Application.Modules.CuentaEvents.GetById;

public record GetByIdCuentaQuery : IRequest<ErrorOr<CuentaDto>>
{
    public GetByIdCuentaQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}