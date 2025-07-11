﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Incidencias
    {
        public int IdIncidencia { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdTipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraCreacion { get; set; }
        public DateTime FechaYHoraResolucion { get; set; }
        public string Resolucion { get; set; }
        public int IdPrioridad { get; set; }
        public string EstadoActual { get; set; }
    }
}
