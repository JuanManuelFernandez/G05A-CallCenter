CREATE DATABASE TPI_CallCenter
GO
USE TPI_CallCenter
GO

CREATE TABLE Incidencias(
	IdIncidencia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	LegajoEmpleado VARCHAR(7) NOT NULL,
	DNICliente Varchar(20) NOT NULL,
	Descripcion VARCHAR(500) NOT NULL,
	FechaYHora datetime NOT NULL,
	IdPrioridad INT NOT NULL,
	Estado BIT NOT NULL
)