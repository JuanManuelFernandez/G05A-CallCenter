GO
USE CallCenter
GO

SELECT * FROM Usuarios
SELECT * FROM Empleados

INSERT INTO Usuarios (TipoUsuario, Email, Clave) VALUES
(3, 'juan.perez@mail.com', 'clave123'),
(3, 'ana.garcia@mail.com', 'secreta456'),
(2, 'julian.alvarez@mail.com', 'empleado'),
(1, 'leo.messi@mail.com', 'admin')

INSERT INTO CategoriasCliente (Nombre, Descripcion) VALUES
('Regular', 'Atenci�n est�ndar en menos de 72h'),
('Super', 'Atenci�n mejorada en menos de 48h'),
('Premium', 'Atenci�n prioritaria en menos de 24h');

INSERT INTO Clientes (IDUsuario, IDCategoria, DNI, Nombre, Apellido, Telefono) VALUES
(3, 1, 30111222, 'Juan', 'Perez', '1122334455');

INSERT INTO Empleados (IDUsuario, Legajo, DNI, Nombre, Apellido) VALUES
(3, 'L0001', 28123456, 'Carlos', 'Lopez')

INSERT INTO TiposIncidente (Nombre, Descripcion) VALUES
('Problema t�cnico', 'Falla en el servicio o software'),
('Consulta general', 'Pregunta o duda sobre el servicio');

INSERT INTO PrioridadesIncidente (Nombre, Descripcion) VALUES
('Alta', 'Requiere atenci�n inmediata'),
('Media', 'Puede esperar hasta 24h'),
('Baja', 'Puede esperar hasta 72h');

INSERT INTO Incidencias (IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion) VALUES
(1, 1, 1, 1, 'Reiniciando router...', 'No puedo acceder al sistema.', GETDATE()),
(2, 2, 2, 2, 'Esperando respuesta de finanzas...', 'Consulta sobre facturaci�n.', GETDATE());

INSERT INTO Historiales (IDIncidencias, FechaCambio) VALUES
(1, GETDATE()),
(2, GETDATE());