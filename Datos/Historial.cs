using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Historial
    {
        public int IdHistorial {  get; set; }
        public int IdIncidencia { get; set; }
        public string EstadoActual { get; set; }
        public string Descripcion { get; set; }
    }
}
