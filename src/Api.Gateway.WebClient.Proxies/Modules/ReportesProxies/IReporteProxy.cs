using Api.Gateway.WebClient.Models.ReporteMgmt.Queries;

namespace Api.Gateway.WebClient.Proxies.Modules.ReportesProxies;

public interface IReporteProxy
{
    Task<HttpResponseMessage> GetEstadoCuentaAsync(EstadoCuentaQuery query);
}