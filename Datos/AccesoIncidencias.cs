using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class AccesoIncidencias
    {
        private List<Incidencias> incidencias = null;
        private AccesoDatos datos = null;

        public List<Incidencias> listar() { 
            incidencias = new List<Incidencias>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT * FROM Incidencias"); //cambiar...
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencias aux = new Incidencias();
                    aux.IdIncidencia = (int)datos.Lector["IdIncidencia"];
                    aux.LegajoEmpleado = (string)datos.Lector["LegajoEmpleado"];
                    aux.DNI = (string)datos.Lector["DNICliente"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FechaYHora = (DateTime)datos.Lector["FechaYHora"];
                    aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];

                    incidencias.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
            return incidencias;
        }
        public Incidencias Buscar(string id) {
            datos = new AccesoDatos();
            Incidencias aux = new Incidencias();
            datos.Conectar();
            datos.Consultar("SELECT * FROM Incidencias WHERE IDIncidencia = " + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read()) {
                    aux.IdIncidencia = (int)datos.Lector["IdIncidencia"];
                    aux.LegajoEmpleado = (string)datos.Lector["LegajoEmpleado"];
                    aux.DNI = (string)datos.Lector["DNICliente"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FechaYHora = (DateTime)datos.Lector["FechaYHora"];
                    aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
            return aux;
        }
    }
}
