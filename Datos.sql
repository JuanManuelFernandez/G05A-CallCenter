--GO
--USE TPI_CallCenter
--GO

--SELECT * FROM X

--INSERT INTO Usuarios (Email, Clave) VALUES
--('juan.perez@mail.com', 'clave123'),
--('ana.garcia@mail.com', 'secreta456')

--INSERT INTO CategoriasCliente (Nombre, Descripcion) VALUES
--('Regular', 'Atención estándar en menos de 72h'),
--('Premium', 'Atención prioritaria en menos de 24h');

--INSERT INTO Clientes (IDUsuario, IDCategoria, DNI, Nombre, Apellido, Telefono) VALUES
--(1, 1, 30111222, 'Juan', 'Perez', '1122334455'),
--(2, 2, 30999888, 'Ana', 'Garcia', '1144556677');

--INSERT INTO Empleados (IDUsuario, Legajo, DNI, Nombre, Apellido) VALUES
--(1, 'LEG001', 28123456, 'Carlos', 'Lopez'),
--(2, 'LEG002', 29123456, 'Lucia', 'Martinez');

---- TiposIncidente
--INSERT INTO TiposIncidente (Nombre, Descripcion) VALUES
--('Problema técnico', 'Falla en el servicio o software'),
--('Consulta general', 'Pregunta o duda sobre el servicio');

---- PrioridadesIncidente
--INSERT INTO PrioridadesIncidente (Nombre, Descripcion) VALUES
--('Alta', 'Requiere atención inmediata'),
--('Baja', 'Puede esperar hasta 72h');

---- Incidencias (Empleado = 1,2 / Cliente = 1,2 / Tipo = 1,2 / Prioridad = 1,2)
--INSERT INTO Incidencias (IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion) VALUES
--(1, 1, 1, 1, 'Abierta', 'No puedo acceder al sistema.', GETDATE()),
--(2, 2, 2, 2, 'En proceso', 'Consulta sobre facturación.', GETDATE());

---- Historiales (IDIncidencias = 1,2)
--INSERT INTO Historiales (IDIncidencias, FechaCambio) VALUES
--(1, GETDATE()),
--(2, GETDATE());