using AutoMapper;
using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientMgmt.Application.Modules.ClienteEvents.GetById;

/// <summary>
/// Manejador para procesar la consulta GetByIdClienteQuery.
/// Recupera un cliente específico por su ID y lo devuelve como un DTO.
/// </summary>
public record class GetByIdClienteQueryHandler : IRequestHandler<GetByIdClienteQuery, ErrorOr<ClienteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor de GetByIdClienteQueryHandler.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo para interactuar con la capa de datos.</param>
    /// <param name="mapper">Instancia de IMapper para mapear entidades a DTOs.</param>
    /// <exception cref="ArgumentNullException">Lanzada si unitOfWork o mapper es nulo.</exception>
    public GetByIdClienteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Maneja la consulta para obtener un cliente por su ID.
    /// </summary>
    /// <param name="request">Consulta que contiene el ID del cliente a recuperar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Un DTO del cliente si se encuentra, o un error si falla.</returns>
    public async Task<ErrorOr<ClienteDto>> Handle(GetByIdClienteQuery request, CancellationToken cancellationToken)
    {
        // Recuperar el cliente por su ID, incluyendo la entidad Persona asociada
        var persona = await _unitOfWork
            .Clientes
            .SingleOrDefaultAsNoTrackingAsync(
                x => x.ClienteId == request.Id,
                include: x => x.Include(y => y.Persona)!
            );

        // Validar si el cliente existe
        if (persona is not Cliente)
        {
            // Devolver un error "No encontrado" si el cliente no existe
            return Error.NotFound("Cliente.NotFound", "No se encontró el cliente con el Id proporcionado.");
        }

        // Mapear la entidad Cliente a un DTO y devolverlo
        return _mapper.Map<ClienteDto>(persona);
    }
}
