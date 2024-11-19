using AccountMgmt.Application.Modules.MovimientoEvents.EstadoCuenta;
using AccountMgmt.Domain.Entities;
using AutoMapper;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Common;

public class MovimientoProfile : Profile
{
    public MovimientoProfile()
    {
        CreateMap<Movimiento, MovimientoDto>();
        CreateMap<Movimiento, EstadoCuentaDto>()
            .ForMember(dest => dest.SaldoInicial, opt => opt.MapFrom(src => src.Saldo))
            .ForMember(dest => dest.NumeroCuenta, opt => opt.MapFrom(src => src.Cuenta!.NumeroCuenta))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Cuenta!.TipoCuenta.ToString()))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Cuenta!.Estado))
            .ForMember(dest => dest.Movimiento, opt => opt.MapFrom(src => src.Valor));
    }
}