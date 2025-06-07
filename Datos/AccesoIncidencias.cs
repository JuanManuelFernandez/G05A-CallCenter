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
                    //IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdTipo = (int)datos.Lector["IDTipo"];
                    aux.IdPrioridad = (int)datos.Lector["IDPrioridad"];
                    aux.EstadoActual = (string)datos.Lector["EstadoActual"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FechaYHoraCreacion = datos.Lector["FechaYHoraCreacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraCreacion"] : DateTime.MaxValue;
                    aux.FechaYHoraResolucion = datos.Lector["FechaYHoraResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraResolucion"] : DateTime.MaxValue;
                    aux.Resolucion = datos.Lector["Resolucion"] != DBNull.Value ? (string)datos.Lector["Resolucion"] : string.Empty;

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
                    //IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdTipo = (int)datos.Lector["IdTipo"];
                    aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];
                    aux.EstadoActual = (string)datos.Lector["EstadoActual"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FechaYHoraCreacion = datos.Lector["FechaYHoraCreacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraCreacion"] : DateTime.MaxValue;
                    aux.FechaYHoraResolucion = datos.Lector["FechaYHoraResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraResolucion"] : DateTime.MaxValue;
                    aux.Resolucion = datos.Lector["Resolucion"] != DBNull.Value ? (string)datos.Lector["Resolucion"] : string.Empty;
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
