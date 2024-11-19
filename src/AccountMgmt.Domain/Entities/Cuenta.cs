using AccountMgmt.Domain.Enums;

namespace AccountMgmt.Domain.Entities;

public class Cuenta
{
    public Guid CuentaId { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public GenericEnums.TipoCuenta TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; } = true;
    public Guid ClienteId { get; set; }
    public ICollection<Movimiento>? Movimientos { get; set; }
}