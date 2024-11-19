using ClientMgmt.Application.Modules.PersonaEvents.Common;

namespace ClientMgmt.Application.Modules.ClienteEvents.Common;

public record ClienteDto
{
    public PersonaDto Persona { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
    public string Contrasena { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}