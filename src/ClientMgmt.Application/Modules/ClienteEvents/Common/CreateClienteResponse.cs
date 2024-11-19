namespace ClientMgmt.Application.Modules.ClienteEvents.Common;

public record CreateClienteResponse 
{
    public CreateClienteResponse(Guid clienteId, Guid personaId)
    {
        ClienteId = clienteId;
        PersonaId = personaId;
    }

    public Guid ClienteId { get; set; }
    public Guid PersonaId { get; set; }
}
