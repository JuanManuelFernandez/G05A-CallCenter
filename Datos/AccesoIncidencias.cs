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

        public List<Incidencia> listar()
        {
            AccesoPrioridades priori = new AccesoPrioridades();
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT IDIncidencia, IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion FROM Incidencias");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdTipo = (int)datos.Lector["IDTipo"];
                    aux.prioridad = new PrioridadesIncidente();
                    aux.prioridad.IDPrioridad = (int)datos.Lector["IDPrioridad"];
                    aux.prioridad.Nombre = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Nombre;
                    aux.prioridad.Descripcion = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Descripcion;
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
        public Incidencia Buscar(string id)
        {
            AccesoPrioridades priori = new AccesoPrioridades();
            datos = new AccesoDatos();
            Incidencia aux = new Incidencia();
            datos.Conectar();
            datos.Consultar("SELECT IDIncidencia, IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion FROM Incidencias WHERE IDIncidencia = " + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdTipo = (int)datos.Lector["IDTipo"];
                    aux.prioridad = new PrioridadesIncidente();
                    aux.prioridad.IDPrioridad = (int)datos.Lector["IDPrioridad"];
                    aux.prioridad.Nombre = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Nombre;
                    aux.prioridad.Descripcion = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Descripcion;
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
        public List<Incidencia> listarIncidenciasCliente(int id)
        {
            AccesoPrioridades priori = new AccesoPrioridades();
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT IDIncidencia, IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion FROM Incidencias WHERE IDCliente =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdTipo = (int)datos.Lector["IDTipo"];
                    aux.prioridad = new PrioridadesIncidente();
                    aux.prioridad.IDPrioridad = (int)datos.Lector["IDPrioridad"];
                    aux.prioridad.Nombre = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Nombre;
                    aux.prioridad.Descripcion = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Descripcion;
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
        public List<Incidencia> listarIncidenciasEmpleado(int id)
        {
            AccesoPrioridades priori = new AccesoPrioridades();
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT IDIncidencia, IDEmpleado, IDCliente, IDTipo, IDPrioridad, EstadoActual, Descripcion, FechaYHoraCreacion, FechaYHoraResolucion, Resolucion FROM Incidencias WHERE IDEmpleado =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];
                    aux.IdTipo = (int)datos.Lector["IDTipo"];
                    aux.prioridad = new PrioridadesIncidente();
                    aux.prioridad.IDPrioridad = (int)datos.Lector["IDPrioridad"];
                    aux.prioridad.Nombre = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Nombre;
                    aux.prioridad.Descripcion = (priori.BuscarPorId(aux.prioridad.IDPrioridad)).Descripcion;
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
    }
}
