using ClientMgmt.Domain.Enums;

namespace ClientMgmt.Application.Modules.PersonaEvents.Common;

public record PersonaDto
{
    public Guid PersonaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public GenericEnums.GeneroPersona Genero { get; set; }
    public int Edad { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}