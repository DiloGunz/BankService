using Api.Gateway.WebClient.Models.AccountMgmt.Common;

namespace Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.DTOs;

public record MovimientoDto
{
    public Guid MovimientoId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public Guid CuentaId { get; set; }
}