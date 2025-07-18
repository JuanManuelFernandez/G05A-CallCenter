﻿using System;
using System.Threading;
using Datos;

namespace Dominio
{
    public class HostedTimerService
    {
        private static Timer _timer;
        private static DateTime? _ultimoEnvio = null;

        public static void Main(string[] args)
        {
            // Ejecuta la lógica cada 1 hora
            _timer = new Timer(CallbackMethod, null, TimeSpan.Zero, TimeSpan.FromHours(1));

        }

        private static void CallbackMethod(object state)
        {
            var ahora = DateTime.Now;

            // Ejecutar SOLO a las 8:00 AM, una sola vez por día
            if (ahora.Hour == 8)
            {
                if (_ultimoEnvio == null || _ultimoEnvio.Value.Date != ahora.Date)
                {
                    try
                    {
                        EnviarReportes();
                        _ultimoEnvio = ahora;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public static void EnviarReportes()
        {
            var emailService = new EmailService();
            var datosInc = new AccesoIncidencias();
            var datosEmp = new AccesoEmpleados();
            var datosUsr = new AccesoUsuario();

            foreach (var empAux in datosEmp.Listar())
            {
                var usrAux = datosUsr.BuscarUsuarioPorId(empAux.IdUsuario);

                var estadoReporte = usrAux.Reporte;
                var idUsuario = usrAux.IdUsuario;

                if (empAux != null &&  datosUsr.BuscarUsuarioPorId(empAux.IdUsuario).Reporte == true)
                {
                    var alta = 0;
                    var media = 0;
                    var baja = 0;
                    var emailBody = string.Empty;

                    //int idUsuario = 
                    var usr = datosUsr.BuscarUsuarioPorId(idUsuario); // Acceso a Email
                    var emp = datosEmp.BuscarPorIdUsuario(idUsuario);
                    foreach (var inc in datosInc.Listar())
                    {
                        if (inc.IdEmpleado == emp.IdEmpleado)
                        {
                            // Calculo de Estadisticas...
                            switch (inc.Prioridad.IdPrioridad)
                            {
                                case 1: alta++; break;
                                case 2: media++; break;
                                case 3: baja++; break;
                            }

                            // Incidencia 1 - Fecha - Esatdo actual - Descripcion
                            // Incidencia 2 - Fecha - Esatdo actual - Descripcion
                            // Incidencia N...
                            emailBody +=
                                "<h1>Incidencia #" + inc.IdIncidencia + ":</h1>" +
                                "<b>Fecha/hora creacion:</b> " + inc.FechaYHoraCreacion + "<br><br>" +
                                "<b>Estado actual:</b>  <br>" +
                                inc.EstadoActual + "<br><br>" +
                                "<b>Descripcion:</b> <br>" +
                                inc.Descripcion + "<br><br>";
                        }
                    }
                    // Cargo Estadisticas al final del mail
                    emailBody +=
                        "<hr>" +
                        "<h2>Estadísticas:</h2>" +
                        "<h3>Prioridad:</h3>" +
                        $"<strong>Alta:</strong> {alta}<br>" +
                        $"<strong>Media:</strong> {media}<br>" +
                        $"<strong>Baja:</strong> {baja}<br>" +
                        $"<h3>Total: {alta + media + baja} </h3>";

                    // Armo el email...
                    emailService.ArmarCorreo
                    (
                        usr.Email,
                        "Reporte Mensual de Incidencias - Empleado #" + emp.IdEmpleado,
                        emailBody
                    );
                    // ...y lo envio
                    try
                    {
                        emailService.EnviarEmail();
                    }
                    catch (Exception ex)
                    {
                        throw ex; // ???
                        //Session.Add("error", ex);
                        //Response.Redirect("Error.aspx");
                    }

                }
            }
        }
        
    }
}