--CREATE DATABASE TPI_CallCenter
--GO
--USE TPI_CallCenter
--GO

--CREATE TABLE Incidencias(
--	IdIncidencia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	LegajoEmpleado VARCHAR(7) NOT NULL,
--	DNICliente Varchar(20) NOT NULL, -- Reemplazar por IDCliente?
--	IdTipo INT NOT NULL, -- Hacer tabla aparte de Tipos de Incidencia con un ID
--	--EstadoActual VARCHAR()
--	Descripcion VARCHAR(500) NOT NULL,
--	FechaYHoraCreacion datetime NOT NULL,
--	FechaYHoraResolucion datetime NULL,
--	Resolucion VARCHAR(500) NULL,
--	IdPrioridad INT NOT NULL, -- Debatir convecion
--	Estado VARCHAR(50) NOT NULL
--)

--PRUEBA PARA TABLA CLIENTES
--CREATE TABLE Cliente (
--	IDPersona INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--	DNI NVARCHAR(50) NOT NULL,
--	Nombre NVARCHAR(50) NOT NULL,
--	Apellido NVARCHAR(50) NOT NULL,
--	Mail NVARCHAR(100) NOT NULL,
--	Telefono NVARCHAR(50) NOT NULL,
--	Contraseña NVARCHAR(50) NOT NULL
--)

SELECT * FROM Cliente;

--DELETE FROM Cliente Where IDPersona = 9;

--INSERT INTO Cliente (DNI, Nombre, Apellido, Mail, Telefono, Contraseña)
--VALUES ('12345678A', 'Juan', 'Pérez', 'juan.perez@example.com', '123456789', 'miContraseñaSegura');

--DROP TABLE Cliente;

--SELECT * FROM Incidencias

--INSERT INTO Incidencias (LegajoEmpleado, DNICliente, IdTipo, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion, IdPrioridad, Estado)
--	VALUES ('e98114', '46286378', 1, 'Rotura de Modem', GETDATE(), NULL, NULL, 1, 'En Progreso')

--table persona{
--  IDPersona int
--  Nombre string
--  Apellido string
--  Contraseña string
--  Mail string
--  Telefono int
--}

--table clientes{
--  Dni int
--  Categoria categoria -- Regular, Plus, Premium
--}

--table empleados{
--  Legajo string
--  Perfil string
--}

--table incidencias{
--  IDIncidencias int
--  LegajoEmpleado string
--  DNICliente string
--  Tipo Tipo
--  Descripcion string
--  FechaYHoraCreacion datetime
--  FechaYHoraResolucion datetime
--  Resolucion string
--  Estado string
--  Prioridad prioridad
--}

--Table tipoIncidente{
--  IdTipo int
--  Descripcion string
--}

--table historial{
--  IDHistorial int
--  IDIncidencias int
--  Legajo string
--  FechaCambio datetime
--}

--table prioridad{
--  IDPrioridad int
--  Descripcion string
--}

--table categoria {
--  IDCategoria int
--  Descripcion string
--}