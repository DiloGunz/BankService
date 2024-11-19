using ErrorOr;
using FluentValidation;
using MediatR;

namespace ClientMgmt.Application.Common.Behaviors;

/// <summary>
/// Comportamiento de la canalización (Pipeline Behavior) para la validación de solicitudes en Mediator.
/// Valida la solicitud utilizando un validador específico antes de continuar con el siguiente manejador.
/// </summary>
/// <typeparam name="TRequest">El tipo de la solicitud que implementa IRequest.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta que implementa IErrorOr.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Constructor que inicializa el comportamiento de validación.
    /// </summary>
    /// <param name="validator">
    /// Instancia del validador para validar la solicitud. Es opcional y puede ser nulo.
    /// </param>
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    /// <summary>
    /// Maneja la validación de la solicitud antes de pasarla al siguiente manejador.
    /// </summary>
    /// <param name="request">La solicitud a procesar.</param>
    /// <param name="next">Delegate que invoca el siguiente manejador en la canalización.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>
    /// La respuesta procesada por el siguiente manejador si la validación es exitosa,
    /// o una lista de errores de validación si la solicitud no es válida.
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

        // Si la solicitud es válida, continuar con el siguiente manejador
        if (validatorResult.IsValid)
        {
            return await next();
        }

        // Convertir los errores de validación en una lista de errores
        var errors = validatorResult.Errors
                    .ConvertAll(validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage
                    ));

        // Devolver los errores como la respuesta
        return (dynamic)errors;
    }
}

