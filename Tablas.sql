CREATE DATABASE TPI_CallCenter
GO
USE TPI_CallCenter
GO

DROP TABLE Incidencias

CREATE TABLE Incidencias(
	IDIncidencia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	IDEmpleado INT NOT NULL, --FOREIGN KEY REFERENCES Empleados(IDEmpleado),
	IDCliente INT NOT NULL, --FOREIGN KEY REFERENCES Clientes(IDCliente),
	IDTipo INT NOT NULL, -- FOREIGN KEY REFERENCES TiposIncidente(IDTipo),
	IDPrioridad INT NOT NULL, -- FOREIGN KEY REFERENCES PrioridadesIncidente(IDPrioridad),
	EstadoActual VARCHAR(250) NOT NULL,
	Descripcion NVARCHAR(500) NOT NULL,
	FechaYHoraCreacion datetime NOT NULL,
	FechaYHoraResolucion datetime NULL,
	Resolucion NVARCHAR(500) NULL
)

INSERT INTO Incidencias (IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion)
VALUES (7, 3, 1, 1, 'Enviamos personal para reemplazar el modem', 'El cliente no uso el adaptador correcto y quemo la fuente del modem', GETDATE(), NULL, NULL),
	   (7, 5, 3, 2, 'Iniciamos proceso de reinicio de fabrica remoto', 'Firmware del modem corrompido tras una actualizacion', GETDATE(), NULL, NULL)

SELECT * FROM Incidencias

--CREATE TABLE TiposIncidente{
--  IDTipo INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--  Descripcion string
--}

--CREATE TABLE PrioridadesIncidente{
--  IDPrioridad INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--  Descripcion VARCHAR
--}

CREATE TABLE Clientes (
	IDCliente INT PRIMARY KEY IDENTITY(1,1) NOT NULL, -- CAMBIO
	DNI INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido NVARCHAR(50) NOT NULL,
	Mail NVARCHAR(100) NOT NULL,
	Telefono NVARCHAR(50) NOT NULL,
	Contraseña NVARCHAR(50) NOT NULL,
	IDCategoria INT NULL -- CAMBIO
)

INSERT INTO Clientes (DNI, Nombre, Apellido, Mail, Telefono, Contraseña, IDCategoria)
VALUES ('45444555', 'Juan', 'Pérez', 'juan.perez@example.com', '11-3255-9638', 'miContraseñaSegura', 2),
	   ('44333222', 'Jose', 'Diaz', 'jose.diaz@example.com', '11-3324-3234', 'aguanteMessi', 3)

--CREATE TABLE Categorias {
--  IDCategoria INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--  Nombre NVARCHAR(50) NOT NULL, -- Regular, Plus, Premium, etc. Determina el tiempo de respuesta minimo por incidencia
--}

--CREATE TABLE Empleados {
--	IDEmpleado INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--  Legajo string
--	IDCliente INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--  Dni INT NOT NULL,
--	Nombre VARCHAR(50) NOT NULL,
--	Apellido VARCHAR(50) NOT NULL,
--	Mail VARCHAR(100) NOT NULL,
--  IdCategoria INT NOT NULL -- Regular, Plus, Premium
--}

--CREATE TABLE historial{
--  IDHistorial int
--  IDIncidencias int
--  Legajo string
--  FechaCambio datetime
--}