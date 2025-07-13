using System;

namespace Datos
{
    public class Incidencias
    {
        public int IdIncidencia { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public TiposIncidente Tipo { get; set; }
        public PrioridadesIncidente Prioridad { get; set; }
        public string EstadoActual { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraCreacion { get; set; }
        public DateTime FechaYHoraResolucion { get; set; }
        public string Resolucion { get; set; }
    }
}
