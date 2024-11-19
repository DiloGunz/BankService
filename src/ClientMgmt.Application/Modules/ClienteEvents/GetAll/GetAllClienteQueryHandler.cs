using AutoMapper;
using ClientMgmt.Application.Modules.ClienteEvents.Common;
using ClientMgmt.Domain.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientMgmt.Application.Modules.ClienteEvents.GetAll;

/// <summary>
/// Manejador para procesar la consulta GetAllClienteQuery.
/// Se encarga de recuperar la lista de clientes con sus datos asociados.
/// </summary>
public class GetAllClienteQueryHandler : IRequestHandler<GetAllClienteQuery, ErrorOr<IReadOnlyList<ClienteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor de GetAllClienteQueryHandler.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo para interactuar con la capa de datos.</param>
    /// <param name="mapper">Instancia de IMapper para mapear entidades a DTOs.</param>
    /// <exception cref="ArgumentNullException">Lanzada si unitOfWork o mapper es nulo.</exception>
    public GetAllClienteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Maneja la consulta para obtener todos los clientes.
    /// </summary>
    /// <param name="request">Consulta sin parámetros adicionales.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Una lista de objetos ClienteDto o un error si falla.</returns>
    public async Task<ErrorOr<IReadOnlyList<ClienteDto>>> Handle(GetAllClienteQuery request, CancellationToken cancellationToken)
    {
        // Recuperar todos los clientes con su entidad Persona asociada
        var data = await _unitOfWork.Clientes.GetAllAsNoTrackingAsync(include: x => x.Include(y => y.Persona)!);

        // Mapear las entidades Cliente a DTOs
        return _mapper.Map<List<ClienteDto>>(data);
    }
}
