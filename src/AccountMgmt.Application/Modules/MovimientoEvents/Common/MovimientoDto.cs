using AccountMgmt.Domain.Enums;

namespace AccountMgmt.Application.Modules.MovimientoEvents.Common;

public record MovimientoDto
{
    public Guid MovimientoId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public GenericEnums.TipoMovimiento TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public Guid CuentaId { get; set; }
}