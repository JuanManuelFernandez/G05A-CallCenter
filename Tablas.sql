CREATE DATABASE CallCenter
GO
USE CallCenter
GO

--DROP TABLE X
--SELECT * FROM Usuarios
--SELECT * FROM Clientes

CREATE TABLE Usuarios (
	IDUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	TipoUsuario INT NOT NULL, -- 1 = Admin, 2 = Empleado, 3 = Cliente
	Email VARCHAR(100) NOT NULL,
	Clave VARCHAR(100) NOT NULL,
	Eliminado BIT NOT NULL DEFAULT 0
)

CREATE TABLE CategoriasCliente (
  IDCategoria INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  Nombre NVARCHAR(50) NOT NULL, -- Regular, Plus, Premium, etc. Determina el tiempo de respuesta minimo por incidencia
  Descripcion NVARCHAR(250) NULL
)

CREATE TABLE Clientes (
	IDCliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	IDCategoria INT NULL FOREIGN KEY REFERENCES CategoriasCliente(IDCategoria),
	IDUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IDUsuario),
	DNI INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido NVARCHAR(50) NOT NULL,
	Telefono NVARCHAR(50) NOT NULL
)
CREATE TABLE Empleados (
	IDEmpleado INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	IDUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IDUsuario),
	Legajo VARCHAR(10) NOT NULL,
	DNI INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido NVARCHAR(50) NOT NULL
)

CREATE TABLE TiposIncidente (
  IDTipo INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  Nombre NVARCHAR(50) NOT NULL,
  Descripcion NVARCHAR(250) NOT NULL
)

CREATE TABLE PrioridadesIncidente (
  IDPrioridad INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  Nombre NVARCHAR(50) NOT NULL, -- Uno, Dos o Tres
  Descripcion NVARCHAR(250) NOT NULL
)

CREATE TABLE Incidencias (
	IDIncidencia INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	IDEmpleado INT NULL FOREIGN KEY REFERENCES Empleados(IDEmpleado),
	IDCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(IDCliente),
	IDTipo INT NOT NULL FOREIGN KEY REFERENCES TiposIncidente(IDTipo),
	IDPrioridad INT NOT NULL FOREIGN KEY REFERENCES PrioridadesIncidente(IDPrioridad),
	EstadoActual VARCHAR(250) NOT NULL,
	Descripcion NVARCHAR(500) NOT NULL,
	FechaYHoraCreacion datetime NOT NULL,
	FechaYHoraResolucion datetime NULL,
	Resolucion NVARCHAR(500) NULL
)

CREATE TABLE Historiales (
  IDHistorial INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  IDIncidencias INT NOT NULL FOREIGN KEY REFERENCES Incidencias(IDIncidencia),
  FechaCambio datetime
)