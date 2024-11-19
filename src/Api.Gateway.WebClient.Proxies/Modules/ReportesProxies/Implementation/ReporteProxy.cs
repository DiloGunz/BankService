using Api.Gateway.WebClient.Models.ReporteMgmt.Queries;
using Api.Gateway.WebClient.Proxies.Config;
using Api.Gateway.WebClient.Proxies.Extensions;
using Microsoft.Extensions.Options;

namespace Api.Gateway.WebClient.Proxies.Modules.ReportesProxies.Implementation;

public class ReporteProxy : IReporteProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    private readonly string _reporteRoute;

    public ReporteProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
    {
        _apiUrls = apiUrls.Value ?? throw new ArgumentNullException(nameof(apiUrls));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.AddBearerToken();
        _reporteRoute = $"{_apiUrls.AccountMgmt}/api/reporte";
    }

    public async Task<HttpResponseMessage> GetEstadoCuentaAsync(EstadoCuentaQuery query)
    {
        var url = $"{_reporteRoute}/estadocuenta".AddObjectAsQueryString(query);
        var result = await _httpClient.ExecuteGetAsync(url);
        return result;
    }
}