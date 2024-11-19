using Api.Gateway.WebClient.Models.AccountMgmt.Common;

namespace Api.Gateway.WebClient.Models.ClientMgmt.Persona.Commands;

public record CreatePersonaCmd
{
    public string Nombre { get; set; } = string.Empty;
    public GenericEnums.GeneroPersona Genero { get; set; }
    public int Edad { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}