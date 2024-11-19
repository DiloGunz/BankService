using AccountMgmt.Domain.Entities;
using AutoMapper;

namespace AccountMgmt.Application.Modules.CuentaEvents.Common;

public class CuentaProfile : Profile
{
    public CuentaProfile()
    {
        CreateMap<Cuenta, CuentaDto>();
    }
}