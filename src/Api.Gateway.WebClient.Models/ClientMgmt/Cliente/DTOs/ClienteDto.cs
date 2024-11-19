using Api.Gateway.WebClient.Models.ClientMgmt.Persona.DTOs;

namespace Api.Gateway.WebClient.Models.ClientMgmt.Cliente.DTOs;

public record ClienteDto
{
    public PersonaDto Persona { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
    public string Contraseña { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}