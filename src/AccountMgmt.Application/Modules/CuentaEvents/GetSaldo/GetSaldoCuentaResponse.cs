namespace AccountMgmt.Application.Modules.CuentaEvents.GetSaldo;

public record GetSaldoCuentaResponse
{
    public bool Estado { get; set; }
    public decimal Saldo { get; set; }
}