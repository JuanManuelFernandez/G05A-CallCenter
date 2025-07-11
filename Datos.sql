GO
USE CallCenter
GO

INSERT INTO Usuarios (TipoUsuario, Email, Clave) VALUES
(3, 'cliente1.52dfq@silomails.com', 'clave1'), -- Mail de prueba
(3, 'cliente2@mail.com', 'clave2'),
(3, 'cliente3@mail.com', 'clave3'),
(2, 'empleado1.nw9m0@silomails.com', 'clave1'), -- Mail de prueba
(2, 'empleado2@mail.com', 'clave2'),
(2, 'fernandezbogojuanmanuel@gmail.com', 'clave3'),
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
-- Empleado 1
(1, 1, 1, 1, 'Reiniciando router', 'No puedo acceder a internet hace 4 horas.', '2025-07-23 12:30'),
(1, 1, 1, 1, 'Enviando tecnico', 'No puedo acceder a internet hace 4 dias.', '2025-07-26 8:30'),
(1, 1, 1, 1, 'Esperando respuesta del cliente', 'La señal del Wi-Fi se corta cada 10 minutos.', '2025-07-25 10:15'),
(1, 1, 1, 2, 'Configurando nuevo router', 'Se daño el router anterior durante una tormenta.', '2025-07-26 09:00'),
(1, 1, 2, 1, 'Derivado a soporte técnico', 'No funciona el servicio de TV. Pantalla negra.', '2025-07-27 11:30'),
(1, 1, 2, 3, 'Consultando condiciones del contrato', 'Quiero saber si puedo cambiar de plan sin penalidad.', '2025-07-28 16:00'),
-- Empleado 2
(2, 2, 1, 2, 'Instalando actualizacion', 'La conexion es muy lenta. Solo hay 2 dispositivos conectados y ninguno usa Netflix.', '2025-07-23 14:30'),
(2, 2, 1, 2, 'Se trabo la actualizacion', 'Hace ya mas de 3 horas que esta bajando la actualizacion. Esta funcionando?', '2025-07-23 12:30'),
(2, 2, 1, 2, 'Revisando cobertura', 'El internet es intermitente desde anoche.', '2025-07-25 08:45'),
(2, 2, 1, 1, 'Reseteando conexión', 'No tengo acceso a internet ni por cable ni Wi-Fi.', '2025-07-26 13:10'),
(2, 2, 2, 3, 'Escalado a segundo nivel', 'No puedo grabar programas en el decodificador.', '2025-07-27 15:20'),
(2, 2, 2, 2, 'Esperando documentación', 'Estoy intentando dar de baja el servicio pero no recibo respuesta.', '2025-07-28 17:40'),
-- Empleado 3
(3, 3, 2, 3, 'Consulta por mail a finanzas', 'Puedo pagar por Mercadopago en vez de transferencia bancaria?', '2025-07-23 18:30'),
(3, 3, 2, 3, 'Consulta por mail a cobranzas', 'Me estan cobrando por servicios que no uso. Quiero pedir la baja ya mismo!', '2025-07-23 9:30'),
(3, 3, 2, 2, 'Realizando pruebas remotas', 'No se escucha audio en ciertos canales.', '2025-07-25 11:20'),
(3, 3, 2, 1, 'En espera de confirmación del cliente', 'Quiero pasar de plan básico a plan plus.', '2025-07-26 14:55'),
(3, 3, 1, 2, 'Revisión programada', 'Internet se desconecta cada vez que llueve.', '2025-07-27 10:40'),
(3, 3, 2, 3, 'Contactando al área de facturación', 'Me llegó una factura con doble cargo por el mismo mes.', '2025-07-28 09:15')

INSERT INTO Plantillas (Nombre, Descripcion) VALUES
('Caso inactivo', 'Hola, como estas?' +CHAR(13)+CHAR(13)+
'Hace un tiempo que no tenemos novedades de vos. Tuviste tiempo para ver nuestros anteriores mensajes sobre este caso?' +CHAR(13)+
'Recorda que siempre estamos aca para ayudarte.'  +CHAR(13)+CHAR(13)+
'Muchas gracias,'  +CHAR(13)+
'Soporte UTN'),
('Completar datos', 'Hola, como estas?' +CHAR(10)+CHAR(10)+
'Para facilitar la gestion de este caso, por favor completa la siguiente informacion:' +CHAR(10)+
'DNI: ' +CHAR(10)+
'Nombre y Apellido: ' +CHAR(10)+
'Telefono: ' +CHAR(10)+
'Direccion: '  +CHAR(10)+CHAR(10)+
'envia la información solicitada a este mail: utncallcenter@gmail.com' + CHAR(10) +
'Muchas gracias,'  +CHAR(10)+
'Soporte UTN'),
('Derivaciones', 'Gracias por contactarte con nosotros.' +CHAR(13)+CHAR(13)+
'Vamos a enviar tu caso a un Ingeniero de Soporte, quien se pondra pronto en contacto para ayudarte con tus inconvenientes.' +CHAR(13)+CHAR(13)+
'Muchas gracias,' +CHAR(13)+
'Soporte UTN')

--DELETE FROM Incidencias
--DELETE FROM PrioridadesIncidente
--DELETE FROM TiposIncidente
--DELETE FROM Empleados
--DELETE FROM Clientes
--DELETE FROM CategoriasCliente
--DELETE FROM Usuarios
--DELETE FROM Plantillas

--SELECT * FROM Plantillas