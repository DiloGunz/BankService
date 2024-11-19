using ErrorOr;
using FluentValidation;
using MediatR;

namespace ClientMgmt.Application.Common.Behaviors;

/// <summary>
/// Comportamiento de la canalizaci�n (Pipeline Behavior) para la validaci�n de solicitudes en Mediator.
/// Valida la solicitud utilizando un validador espec�fico antes de continuar con el siguiente manejador.
/// </summary>
/// <typeparam name="TRequest">El tipo de la solicitud que implementa IRequest.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta que implementa IErrorOr.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Constructor que inicializa el comportamiento de validaci�n.
    /// </summary>
    /// <param name="validator">
    /// Instancia del validador para validar la solicitud. Es opcional y puede ser nulo.
    /// </param>
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    /// <summary>
    /// Maneja la validaci�n de la solicitud antes de pasarla al siguiente manejador.
    /// </summary>
    /// <param name="request">La solicitud a procesar.</param>
    /// <param name="next">Delegate que invoca el siguiente manejador en la canalizaci�n.</param>
    /// <param name="cancellationToken">Token para cancelar la operaci�n asincr�nica.</param>
    /// <returns>
    /// La respuesta procesada por el siguiente manejador si la validaci�n es exitosa,
    /// o una lista de errores de validaci�n si la solicitud no es v�lida.
    /// </returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Si no hay un validador, continuar con el siguiente manejador
        if (_validator is null)
        {
            return await next();
        }

        // Validar la solicitud
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        // Si la solicitud es v�lida, continuar con el siguiente manejador
        if (validatorResult.IsValid)
        {
            return await next();
        }

        // Convertir los errores de validaci�n en una lista de errores
        var errors = validatorResult.Errors
                    .ConvertAll(validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage
                    ));

        // Devolver los errores como la respuesta
        return (dynamic)errors;
    }
}

