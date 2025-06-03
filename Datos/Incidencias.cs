using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Incidencias
    {
        public int IdIncidencia { get; set; }
        public string LegajoEmpleado { get; set; }
        public string DNI { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHora { get; set; }
        public int IdPrioridad { get; set; } // cambiar...
    }
}
