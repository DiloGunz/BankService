using Api.Gateway.WebClient.Models.ClientMgmt.Persona.Commands;

namespace Api.Gateway.WebClient.Models.ClientMgmt.Cliente.Commands;

public record CreateClienteCmd
{
    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
    public string Contraseña { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
    public CreatePersonaCmd Persona { get; set; }
}