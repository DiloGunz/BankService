using Api.Gateway.WebClient.Middlewares;
using Api.Gateway.WebClient.Proxies.Config;
using Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies;
using Api.Gateway.WebClient.Proxies.Modules.AccountMgmtProxies.Implementation;
using Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies;
using Api.Gateway.WebClient.Proxies.Modules.ClientMgmtProxies.Implementation;
using Api.Gateway.WebClient.Proxies.Modules.ReportesProxies;
using Api.Gateway.WebClient.Proxies.Modules.ReportesProxies.Implementation;

namespace Api.Gateway.WebClient.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddTransient<GloblalExceptionHandlingMiddleware>();

        services.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));

        services.AddHttpClient<IClienteProxy, ClienteProxy>();
        services.AddHttpClient<ICuentaProxy, CuentaProxy>();
        services.AddHttpClient<IMovimientoProxy, MovimientoProxy>();
        services.AddHttpClient<IReporteProxy, ReporteProxy>();

        return services;
    }
}