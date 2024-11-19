using Api.Gateway.WebClient.Models.ClientMgmt.Persona.Commands;

namespace Api.Gateway.WebClient.Models.ClientMgmt.Cliente.Commands;

public record UpdateClienteCmd
{
    public Guid ClienteId { get; set; }
    public UpdatePersonaCmd Persona { get; set; }
    public string Contrasena { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}