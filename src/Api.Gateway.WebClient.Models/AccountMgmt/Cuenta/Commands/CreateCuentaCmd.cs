using Api.Gateway.WebClient.Models.AccountMgmt.Common;

namespace Api.Gateway.WebClient.Models.AccountMgmt.Cuenta.Commands;

public record CreateCuentaCmd
{
    public string NumeroCuenta { get; set; } = string.Empty;
    public GenericEnums.TipoCuenta TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; } = true;
    public Guid ClienteId { get; set; }

}