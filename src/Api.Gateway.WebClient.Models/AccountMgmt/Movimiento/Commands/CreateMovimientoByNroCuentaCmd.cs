namespace Api.Gateway.WebClient.Models.AccountMgmt.Movimiento.Commands;

public record CreateMovimientoByNroCuentaCmd
{
    public decimal Valor { get; set; }
    public string NumeroCuenta { get; set; }
}