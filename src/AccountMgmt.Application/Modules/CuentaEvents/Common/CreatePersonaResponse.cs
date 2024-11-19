namespace AccountMgmt.Application.Modules.CuentaEvents.Common;

public record CreatePersonaResponse
{
    public CreatePersonaResponse(Guid clienteId, Guid personaId)
    {
        ClienteId = clienteId;
        PersonaId = personaId;
    }

    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
}
