using Api.Gateway.WebClient.Models.ClientMgmt.Cliente.Commands;

namespace Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies;

public interface IClienteProxy
{
    Task<HttpResponseMessage> GetAllAsync();
    Task<HttpResponseMessage> GetByIdAsync(Guid id);
    Task<HttpResponseMessage> CreateAsync(CreateClienteCmd command);
    Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateClienteCmd command);
    Task<HttpResponseMessage> DeleteAsync(Guid id);
}