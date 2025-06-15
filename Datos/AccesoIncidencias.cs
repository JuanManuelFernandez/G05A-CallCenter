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
        private List<Incidencia> incidencias = null;
        private AccesoDatos datos = null;

        public List<Incidencia> listar() { 
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT * FROM Incidencias"); //cambiar...
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia
                    {
                        //IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion
                        IdIncidencia = (int)datos.Lector["IDIncidencia"],
                        IdEmpleado = (int)datos.Lector["IDEmpleado"],
                        IdCliente = (int)datos.Lector["IDCliente"],
                        IdTipo = (int)datos.Lector["IDTipo"],
                        IdPrioridad = (int)datos.Lector["IDPrioridad"],
                        EstadoActual = (string)datos.Lector["EstadoActual"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        FechaYHoraCreacion = datos.Lector["FechaYHoraCreacion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraCreacion"] : DateTime.MaxValue,
                        FechaYHoraResolucion = datos.Lector["FechaYHoraResolucion"] != DBNull.Value ? (DateTime)datos.Lector["FechaYHoraResolucion"] : DateTime.MaxValue,
                        Resolucion = datos.Lector["Resolucion"] != DBNull.Value ? (string)datos.Lector["Resolucion"] : string.Empty
                    };

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
        public Incidencia Buscar(string id) {
            datos = new AccesoDatos();
            Incidencia aux = new Incidencia();
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
