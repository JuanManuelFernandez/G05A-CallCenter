GO
USE CallCenter
GO

--DELETE FROM Historiales
--DELETE FROM Incidencias
--DELETE FROM PrioridadesIncidente
--DELETE FROM TiposIncidente
--DELETE FROM Empleados
--DELETE FROM Clientes
--DELETE FROM CategoriasCliente
--DELETE FROM Usuarios

--SELECT * FROM CategoriasCliente

INSERT INTO Usuarios (TipoUsuario, Email, Clave) VALUES
(3, 'usuario1@mail.com', 'clave1'),
(3, 'usuario2@mail.com', 'clave2'),
(3, 'usuario3@mail.com', 'clave3'),
(2, 'empleado1@mail.com', 'clave1'),
(2, 'empleado2@mail.com', 'clave2'),
(2, 'empleado3@mail.com', 'clave3'),
(1, 'admin@mail.com', 'clave1')

INSERT INTO CategoriasCliente (Nombre, Descripcion) VALUES
('Regular', 'Atención estándar en menos de 72h'),
('Super', 'Atención mejorada en menos de 48h'),
('Premium', 'Atención prioritaria en menos de 24h')

INSERT INTO Clientes (IDUsuario, IDCategoria, DNI, Nombre, Apellido, Telefono) VALUES
(1, 1, 10200300, 'Cliente1', 'Uno', '1122223333'),
(2, 2, 40500600, 'Cliente2', 'Dos', '1144445555'),
(3, 3, 40500600, 'Cliente3', 'Tres', '1166667777')

INSERT INTO Empleados (IDUsuario, Legajo, DNI, Nombre, Apellido) VALUES
(4, 'L0001', 70800900, 'Empleado1', 'Uno'),
(5, 'L0002', 10500900, 'Empleado2', 'Dos'),
(6, 'L0003', 10500900, 'Empleado3', 'Tres');

INSERT INTO TiposIncidente (Nombre, Descripcion) VALUES
('Problema técnico', 'Falla en el servicio o software'),
('Consulta general', 'Pregunta o duda sobre el servicio')

INSERT INTO PrioridadesIncidente (Nombre, Descripcion) VALUES
('Alta', 'Requiere atención inmediata'),
('Media', 'Puede esperar hasta 24h'),
('Baja', 'Puede esperar hasta 72h')

INSERT INTO Incidencias (IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion) VALUES
(1, 1, 1, 1, 'Reiniciando router...', 'No puedo acceder a internet.', GETDATE()),
(2, 2, 1, 2, 'Instalando actualizacion...', 'La conexion es muy lenta.', GETDATE()),
(3, 3, 2, 3, 'Consulta sobre facturacion', 'Esperando respuesta de gente de finanzas.', GETDATE())

INSERT INTO Historiales (IDIncidencias, FechaCambio) VALUES
(1, GETDATE()),
(2, GETDATE()),
(3, GETDATE())