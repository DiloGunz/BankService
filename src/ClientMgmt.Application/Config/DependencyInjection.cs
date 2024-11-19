using ClientMgmt.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ClientMgmt.Application.Config;

/// <summary>
/// Clase de configuración para registrar servicios de la capa de aplicación en el contenedor de inyección de dependencias.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Método de extensión para registrar los servicios de la capa de aplicación.
    /// </summary>
    /// <param name="services">El contenedor de servicios de tipo <see cref="IServiceCollection"/>.</param>
    /// <returns>El contenedor de servicios con los servicios de la aplicación registrados.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar MediatR y descubrir manejadores desde el ensamblado de la capa de aplicación
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        // Registrar AutoMapper utilizando el ensamblado de la capa de aplicación
        services.AddAutoMapper(ApplicationAssemblyReference.Assembly);

        // Registrar el comportamiento de validación (ValidationBehavior) para MediatR
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        // Registrar todos los validadores de FluentValidation ubicados en el ensamblado de la capa de aplicación
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }
}
