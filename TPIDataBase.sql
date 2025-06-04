CREATE DATABASE TPI_CallCenter
GO
USE TPI_CallCenter
GO

CREATE TABLE Incidencias(
	IdIncidencia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	LegajoEmpleado VARCHAR(7) NOT NULL,
	DNICliente Varchar(20) NOT NULL,
	IdTipo INT NOT NULL, --Hacer tabla aparte de Tipos de Incidencia con un ID
	Descripcion VARCHAR(500) NOT NULL,
	FechaYHoraCreacion datetime NOT NULL,
	FechaYHoraResolucion datetime NULL,
	Resolucion VARCHAR(500) NULL,
	IdPrioridad INT NOT NULL,
	Estado VARCHAR(50) NOT NULL
)

--SELECT * FROM Incidencias

--INSERT INTO Incidencias (LegajoEmpleado, DNICliente, IdTipo, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion, IdPrioridad, Estado)
--	VALUES ('e98114', '46286378', 1, 'Rotura de Modem', GETDATE(), NULL, NULL, 1, 'En Progreso')