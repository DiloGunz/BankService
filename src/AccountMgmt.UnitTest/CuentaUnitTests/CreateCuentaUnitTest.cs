using AccountMgmt.Application.Modules.CuentaEvents.Create;
using AccountMgmt.Domain.Entities;
using AccountMgmt.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace AccountMgmt.UnitTest.CuentaUnitTests;

/// <summary>
/// Clase de pruebas unitarias para el manejo de creación de cuentas (CreateCuentaCmdHandler).
/// Valida escenarios como la existencia de un número de cuenta duplicado y la creación de nuevas cuentas.
/// </summary>
[TestClass]
public sealed class CreateCuentaUnitTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private CreateCuentaCmdHandler _handler;

    /// <summary>
    /// Configuración inicial para las pruebas unitarias.
    /// Se inicializan los mocks necesarios y el manejador de comandos.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCuentaCmdHandler(_mockUnitOfWork.Object);
    }

    /// <summary>
    /// Prueba que valida que, si el número de cuenta ya existe, el sistema devuelve un error de validación.
    /// </summary>
    /// <returns>Tarea asíncrona que representa la ejecución de la prueba.</returns>
    [TestMethod]
    public async Task Handle_WhenNumeroCuentaExists_ReturnsValidationError()
    {
        var request = new CreateCuentaCmd
        {
            NumeroCuenta = "12345",
            TipoCuenta = Domain.Enums.GenericEnums.TipoCuenta.Ahorros,
            SaldoInicial = 1000,
            Estado = true,
            ClienteId = Guid.NewGuid(),
        };

        _mockUnitOfWork
            .Setup(x => x.Cuentas.AnyAsync(It.IsAny<Expression<Func<Cuenta, bool>>>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsTrue(result.IsError);
        Assert.AreEqual("Cuenta.NumeroCuenta", result.FirstError.Code);
        Assert.AreEqual("El número de cuenta ingresado ya existe.", result.FirstError.Description);
    }

    /// <summary>
    /// Prueba que valida que, si el número de cuenta no existe, el sistema crea la cuenta correctamente.
    /// </summary>
    /// <returns>Tarea asíncrona que representa la ejecución de la prueba.</returns>
    [TestMethod]
    public async Task Handle_WhenNumeroCuentaDoesNotExist_CreatesCuenta()
    {
        var request = new CreateCuentaCmd
        {
            NumeroCuenta = "54321",
            TipoCuenta = Domain.Enums.GenericEnums.TipoCuenta.Corriente,
            SaldoInicial = 2000,
            Estado = true,
            ClienteId = Guid.NewGuid(),
        };

        _mockUnitOfWork
            .Setup(x => x.Cuentas.AnyAsync(It.IsAny<Expression<Func<Cuenta, bool>>>()))
            .ReturnsAsync(false);

        _mockUnitOfWork
            .Setup(x => x.Cuentas.AddAsync(It.IsAny<Cuenta>()))
            .Returns(Task.CompletedTask);

        _mockUnitOfWork
            .Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.IsFalse(result.IsError);
        Assert.IsInstanceOfType(result.Value, typeof(Guid));
        _mockUnitOfWork.Verify(x => x.Cuentas.AddAsync(It.IsAny<Cuenta>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

}