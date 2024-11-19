using ClientMgmt.Application.Modules.PersonaEvents.Common;

namespace ClientMgmt.Application.Modules.ClienteEvents.Common;

public record ClienteDto
{
    public PersonaDto Persona { get; set; }
    public int MyProperty { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
    public string Contraseña { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}