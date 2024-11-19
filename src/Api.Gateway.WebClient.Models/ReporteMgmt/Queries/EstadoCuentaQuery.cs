namespace Api.Gateway.WebClient.Models.ReporteMgmt.Queries;

public record EstadoCuentaQuery(DateTimeOffset FechaInicio, DateTimeOffset FechaFin, Guid ClienteId);