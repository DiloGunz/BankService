-- Crear la base de datos
CREATE DATABASE BankServicesDB;
GO

-- Usar la base de datos
USE BankServicesDB;
GO

-- Crear esquemas
CREATE SCHEMA client_mgmt;
GO

CREATE SCHEMA account_mgmt;
GO

-- Esquema: client_mgmt

-- Tabla: Persona
CREATE TABLE client_mgmt.Persona (
    PersonaId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Genero NVARCHAR(10) NOT NULL,
    Edad INT NOT NULL,
    Identificacion NVARCHAR(50) NOT NULL UNIQUE,
    Direccion NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL
);

-- Tabla: Cliente
CREATE TABLE client_mgmt.Cliente (
    ClienteId UNIQUEIDENTIFIER PRIMARY KEY, -- También es la clave primaria
    PersonaId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES client_mgmt.Persona(PersonaId),
    Contraseña NVARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- Esquema: account_mgmt

-- Tabla: Cuenta
CREATE TABLE account_mgmt.Cuenta (
    CuentaId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NumeroCuenta NVARCHAR(20) NOT NULL UNIQUE,
    TipoCuenta INT NOT NULL, -- Enum almacenado como entero
    SaldoInicial DECIMAL(18,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    ClienteId UNIQUEIDENTIFIER NOT NULL -- Referencia lógica, no relación foránea
);

-- Crear índices
CREATE INDEX IX_NumeroCuenta ON account_mgmt.Cuenta (NumeroCuenta);
CREATE INDEX IX_ClienteId ON account_mgmt.Cuenta (ClienteId);

-- Tabla: Movimiento
CREATE TABLE account_mgmt.Movimiento (
    MovimientoId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    TipoMovimiento INT NOT NULL, -- Enum almacenado como entero
    Valor DECIMAL(18,2) NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL,
    CuentaId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES account_mgmt.Cuenta(CuentaId)
);

-- Crear índices
CREATE INDEX IX_CuentaId ON account_mgmt.Movimiento (CuentaId);

-- Insertar datos de ejemplo

-- Insertar datos en Persona
INSERT INTO client_mgmt.Persona (PersonaId, Nombre, Genero, Edad, Identificacion, Direccion, Telefono)
VALUES 
(NEWID(), 'Jose Lema', 'Masculino', 30, '12345678', 'Otavalo sn y principal', '098254785'),
(NEWID(), 'Marianela Montalvo', 'Femenino', 25, '87654321', 'Amazonas y NN.UU', '0975489656'),
(NEWID(), 'Juan Osorio', 'Masculino', 35, '12349876', '13 junio y Equinoccial', '098874587');

-- Insertar datos en Cliente
INSERT INTO client_mgmt.Cliente (ClienteId, PersonaId, Contraseña, Estado)
VALUES 
(NEWID(), (SELECT TOP 1 PersonaId FROM client_mgmt.Persona WHERE Nombre = 'Jose Lema'), '1234', 1),
(NEWID(), (SELECT TOP 1 PersonaId FROM client_mgmt.Persona WHERE Nombre = 'Marianela Montalvo'), '5678', 1),
(NEWID(), (SELECT TOP 1 PersonaId FROM client_mgmt.Persona WHERE Nombre = 'Juan Osorio'), '1245', 1);

-- Insertar datos en Cuenta
-- TipoCuenta: 1 = Ahorros, 2 = Corriente
INSERT INTO account_mgmt.Cuenta (CuentaId, NumeroCuenta, TipoCuenta, SaldoInicial, Estado, ClienteId)
VALUES 
(NEWID(), '478758', 1, 2000, 1, (SELECT TOP 1 ClienteId FROM client_mgmt.Cliente WHERE Contraseña = '1234')),
(NEWID(), '225487', 2, 100, 1, (SELECT TOP 1 ClienteId FROM client_mgmt.Cliente WHERE Contraseña = '5678')),
(NEWID(), '495878', 1, 0, 1, (SELECT TOP 1 ClienteId FROM client_mgmt.Cliente WHERE Contraseña = '1245')),
(NEWID(), '496825', 1, 540, 1, (SELECT TOP 1 ClienteId FROM client_mgmt.Cliente WHERE Contraseña = '5678'));

-- Insertar datos en Movimiento
-- TipoMovimiento: 1 = Deposito, 2 = Retiro
INSERT INTO account_mgmt.Movimiento (MovimientoId, Fecha, TipoMovimiento, Valor, Saldo, CuentaId)
VALUES
(NEWID(), GETDATE(), 2, -575, 1425, (SELECT TOP 1 CuentaId FROM account_mgmt.Cuenta WHERE NumeroCuenta = '478758')),
(NEWID(), GETDATE(), 1, 600, 700, (SELECT TOP 1 CuentaId FROM account_mgmt.Cuenta WHERE NumeroCuenta = '225487')),
(NEWID(), GETDATE(), 1, 150, 150, (SELECT TOP 1 CuentaId FROM account_mgmt.Cuenta WHERE NumeroCuenta = '495878')),
(NEWID(), GETDATE(), 2, -540, 0, (SELECT TOP 1 CuentaId FROM account_mgmt.Cuenta WHERE NumeroCuenta = '496825'));
