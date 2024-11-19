using Api.Gateway.WebClient.Models.AccountMgmt.Common;

namespace Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.Commands;

public record UpdateMovimientoCmd
{
    public Guid MovimientoId { get; set; }
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
}