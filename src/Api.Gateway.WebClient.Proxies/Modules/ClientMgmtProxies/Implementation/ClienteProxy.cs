using Api.Gateway.WebClient.Models.ClientMgmt.Cliente.Commands;
using Api.Gateway.WebClient.Proxies.Config;
using Api.Gateway.WebClient.Proxies.Extensions;
using Microsoft.Extensions.Options;

namespace Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies.Implementation;

public class ClienteProxy : IClienteProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    private readonly string _clienteRoute;

    public ClienteProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
    {
        _apiUrls = apiUrls.Value ?? throw new ArgumentNullException(nameof(apiUrls));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.AddBearerToken();
        _clienteRoute = $"{_apiUrls.ClientMgmt}/api/cliente";
    }

    public async Task<HttpResponseMessage> CreateAsync(CreateClienteCmd command)
    {
        var request = await _httpClient.ExecutePostAsync(_clienteRoute, command);
        return request;
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id)
    {
        var request = await _httpClient.ExecuteDeleteAsync($"{_clienteRoute}/{id}");
        return request;
    }

    public async Task<HttpResponseMessage> GetAllAsync()
    {
        var request = await _httpClient.GetAsync(_clienteRoute);
        return request;
    }

    public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
    {
        var request = await _httpClient.GetAsync($"{_clienteRoute}/{id}");
        return request;
    }

    public async Task<HttpResponseMessage> UpdateAsync(Guid id, UpdateClienteCmd command)
    {
        var request = await _httpClient.ExecutePutAsync($"{_clienteRoute}/{id}", command);
        return request;
    }
}