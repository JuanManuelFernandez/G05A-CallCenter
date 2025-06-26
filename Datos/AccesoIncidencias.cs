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
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];

                    aux.tipo = new TiposIncidente
                    {
                        IDTipo = (int)datos.Lector["IDTipo"],
                        Nombre = (string)datos.Lector["NombreTipo"],
                        Descripcion = (string)datos.Lector["DescripcionTipo"]
                    };

                    aux.prioridad = new PrioridadesIncidente
                    {
                        IDPrioridad = (int)datos.Lector["IDPrioridad"],
                        Nombre = (string)datos.Lector["NombrePriori"],
                        Descripcion = (string)datos.Lector["DescripcionPriori"]
                    };

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
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad WHERE IDIncidencia = " + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];

                    aux.tipo = new TiposIncidente
                    {
                        IDTipo = (int)datos.Lector["IDTipo"],
                        Nombre = (string)datos.Lector["NombreTipo"],
                        Descripcion = (string)datos.Lector["DescripcionTipo"]
                    };

                    aux.prioridad = new PrioridadesIncidente
                    {
                        IDPrioridad = (int)datos.Lector["IDPrioridad"],
                        Nombre = (string)datos.Lector["NombrePriori"],
                        Descripcion = (string)datos.Lector["DescripcionPriori"]
                    };

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
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad WHERE IDCliente =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];

                    aux.tipo = new TiposIncidente
                    {
                        IDTipo = (int)datos.Lector["IDTipo"],
                        Nombre = (string)datos.Lector["NombreTipo"],
                        Descripcion = (string)datos.Lector["DescripcionTipo"]
                    };

                    aux.prioridad = new PrioridadesIncidente
                    {
                        IDPrioridad = (int)datos.Lector["IDPrioridad"],
                        Nombre = (string)datos.Lector["NombrePriori"],
                        Descripcion = (string)datos.Lector["DescripcionPriori"]
                    };

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
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad WHERE IDEmpleado =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();
                    aux.IdIncidencia = (int)datos.Lector["IDIncidencia"];
                    aux.IdEmpleado = (int)datos.Lector["IDEmpleado"];
                    aux.IdCliente = (int)datos.Lector["IDCliente"];

                    aux.tipo = new TiposIncidente
                    {
                        IDTipo = (int)datos.Lector["IDTipo"],
                        Nombre = (string)datos.Lector["NombreTipo"],
                        Descripcion = (string)datos.Lector["DescripcionTipo"]
                    };

                    aux.prioridad = new PrioridadesIncidente
                    {
                        IDPrioridad = (int)datos.Lector["IDPrioridad"],
                        Nombre = (string)datos.Lector["NombrePriori"],
                        Descripcion = (string)datos.Lector["DescripcionPriori"]
                    };

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
        public void AgregarIncidencia(Incidencia nueva)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO INCIDENCIAS (IDEMPLEADO, IDCLIENTE, IDTIPO, IDPRIORIDAD, ESTADOACTUAL, DESCRIPCION, FECHAYHORACREACION) VALUES (@IDEMPLEADO, @IDCLIENTE, @IDTIPO, @IDPRIORIDAD, @ESTADOACTUAL, @DESCRIPCION, @FECHAYHORACREACION)");
                datos.setearParametro("@IDEMPLEADO",DBNull.Value);
                datos.setearParametro("@IDCLIENTE",nueva.IdCliente);
                datos.setearParametro("@IDTIPO",nueva.tipo.IDTipo);
                datos.setearParametro("@IDPRIORIDAD",nueva.prioridad.IDPrioridad);
                datos.setearParametro("@ESTADOACTUAL","Pendiente");
                datos.setearParametro("@DESCRIPCION",nueva.Descripcion);
                datos.setearParametro("@FECHAYHORACREACION",DateTime.Now);
                datos.EjecutarNonQuery();
            }
            catch (Exception er)
            {

                throw er;
            }
        }
    }
}
