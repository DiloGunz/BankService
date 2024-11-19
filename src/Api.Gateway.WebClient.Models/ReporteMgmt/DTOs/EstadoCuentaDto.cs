namespace Api.Gateway.WebClient.Models.ReporteMgmt.DTOs;

public record EstadoCuentaDto
{
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; }
    public string NumeroCuenta { get; set; }
    /// <summary>
    /// tipo de cuenta
    /// </summary>
    public string Tipo { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; }
    /// <summary>
    /// valor de movimiento
    /// </summary>
    public decimal Movimiento { get; set; }
    public decimal SaldoDisponible { get; set; }
}