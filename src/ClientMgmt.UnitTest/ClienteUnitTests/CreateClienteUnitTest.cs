using ClientMgmt.Application.Modules.ClienteEvents.Create;
using ClientMgmt.Application.Modules.PersonaEvents.Create;
using ClientMgmt.Domain.Entities;
using ClientMgmt.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace ClientMgmt.UnitTest.ClienteUnitTests;

/// <summary>
/// Clase de pruebas unitarias para validar el comportamiento de CreateClienteCmdHandler.
/// Contiene pruebas para verificar casos donde la identificación ya existe y cuando no.
/// </summary>
[TestClass]
public class CreateClienteUnitTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private CreateClienteCmdHandler _handler;

    /// <summary>
    /// Configuración inicial de las pruebas.
    /// Se inicializan los mocks necesarios y el manejador (handler) de comandos.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateClienteCmdHandler(_mockUnitOfWork.Object);
    }

    /// <summary>
    /// Prueba que valida el caso en el que la identificación de la persona ya existe.
    /// El sistema debe devolver un error de validación indicando que la identificación es duplicada.
    /// </summary>
    /// <returns>Tarea asíncrona que representa la ejecución de la prueba.</returns>
    [TestMethod]
    public async Task Handle_WhenIdentificacionExists_ReturnsValidationError()
    {
        var request = new CreateClienteCmd
        {
            Persona = new CreatePersonaCmd
            {
                Identificacion = "123456",
                Nombre = "John Doe",
                Genero = Domain.Enums.GenericEnums.GeneroPersona.Masculino,
                Edad = 30,
                Direccion = "Calle Falsa 123",
                Telefono = "555-1234"
            },
            Contraseña = "password123",
            Estado = true
        };

        _mockUnitOfWork
            .Setup(x => x.Personas.AnyAsync(It.IsAny<Expression<Func<Persona, bool>>>()))
            .ReturnsAsync(true); 

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsTrue(result.IsError);
        Assert.AreEqual("Persona.Identificacion", result.FirstError.Code);
        Assert.AreEqual("El código de identificación ya existe.", result.FirstError.Description);
    }

    /// <summary>
    /// Prueba que valida el caso en el que la identificación de la persona no existe.
    /// El sistema debe crear un nuevo cliente y persistir los cambios correctamente.
    /// </summary>
    /// <returns>Tarea asíncrona que representa la ejecución de la prueba.</returns>
    [TestMethod]
    public async Task Handle_WhenIdentificacionDoesNotExist_CreatesCliente()
    {
        var request = new CreateClienteCmd
        {
            Persona = new CreatePersonaCmd
            {
                Identificacion = "654321",
                Nombre = "Jane Doe",
                Genero = Domain.Enums.GenericEnums.GeneroPersona.Femenino,
                Edad = 25,
                Direccion = "Calle Verdadera 456",
                Telefono = "555-6789"
            },
            Contraseña = "password456",
            Estado = true
        };

        _mockUnitOfWork
            .Setup(x => x.Personas.AnyAsync(It.IsAny<Expression<Func<Persona, bool>>>()))
            .ReturnsAsync(false);

        _mockUnitOfWork
            .Setup(x => x.Clientes.AddAsync(It.IsAny<Cliente>()))
            .Returns(Task.CompletedTask);

        _mockUnitOfWork
            .Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask); 

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsFalse(result.IsError);
        Assert.IsNotNull(result.Value);
        _mockUnitOfWork.Verify(x => x.Clientes.AddAsync(It.IsAny<Cliente>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}