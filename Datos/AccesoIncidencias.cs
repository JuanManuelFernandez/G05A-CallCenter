using System;
using System.Collections.Generic;
using Dominio;

namespace Datos
{
    public class AccesoIncidencias
    {
        private List<Incidencia> incidencias = null;
        private AccesoDatos datos = null;

        public List<Incidencia> Listar()
        {
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia
                    {
                        IdIncidencia = (int)datos.Lector["IDIncidencia"],
                        IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0,
                        IdCliente = (int)datos.Lector["IDCliente"],

                        Tipo = new TiposIncidente
                        {
                            IDTipo = (int)datos.Lector["IDTipo"],
                            Nombre = (string)datos.Lector["NombreTipo"],
                            Descripcion = (string)datos.Lector["DescripcionTipo"]
                        },

                        Prioridad = new PrioridadesIncidente
                        {
                            IDPrioridad = (int)datos.Lector["IDPrioridad"],
                            Nombre = (string)datos.Lector["NombrePriori"],
                            Descripcion = (string)datos.Lector["DescripcionPriori"]
                        },

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
        public Incidencia Buscar(string id)
        {
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
                    aux.IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0;
                    aux.IdCliente = (int)datos.Lector["IDCliente"];

                    aux.Tipo = new TiposIncidente
                    {
                        IDTipo = (int)datos.Lector["IDTipo"],
                        Nombre = (string)datos.Lector["NombreTipo"],
                        Descripcion = (string)datos.Lector["DescripcionTipo"]
                    };

                    aux.Prioridad = new PrioridadesIncidente
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
        public List<Incidencia> ListarIncidenciasCliente(int id)
        {
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad WHERE I.Resolucion IS NULL AND IDCliente =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia
                    {
                        IdIncidencia = (int)datos.Lector["IDIncidencia"],
                        IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0,
                        IdCliente = (int)datos.Lector["IDCliente"],

                        Tipo = new TiposIncidente
                        {
                            IDTipo = (int)datos.Lector["IDTipo"],
                            Nombre = (string)datos.Lector["NombreTipo"],
                            Descripcion = (string)datos.Lector["DescripcionTipo"]
                        },

                        Prioridad = new PrioridadesIncidente
                        {
                            IDPrioridad = (int)datos.Lector["IDPrioridad"],
                            Nombre = (string)datos.Lector["NombrePriori"],
                            Descripcion = (string)datos.Lector["DescripcionPriori"]
                        },

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
        public List<Incidencia> ListarIncidenciasEmpleado(int id)
        {
            incidencias = new List<Incidencia>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT I.IDIncidencia, I.IDEmpleado, I.IDCliente, I.IDTipo, T.Nombre AS NombreTipo, T.Descripcion AS DescripcionTipo, I.IDPrioridad, P.Nombre AS NombrePriori, P.Descripcion AS DescripcionPriori, I.EstadoActual, I.Descripcion, I.FechaYHoraCreacion, I.FechaYHoraResolucion, I.Resolucion FROM Incidencias I INNER JOIN TiposIncidente T ON I.IDTipo = T.IDTipo INNER JOIN PrioridadesIncidente P ON I.IDPrioridad = P.IDPrioridad WHERE I.Resolucion IS NULL AND IDEmpleado =" + id);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia
                    {
                        IdIncidencia = (int)datos.Lector["IDIncidencia"],
                        IdEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0,
                        IdCliente = (int)datos.Lector["IDCliente"],

                        Tipo = new TiposIncidente
                        {
                            IDTipo = (int)datos.Lector["IDTipo"],
                            Nombre = (string)datos.Lector["NombreTipo"],
                            Descripcion = (string)datos.Lector["DescripcionTipo"]
                        },

                        Prioridad = new PrioridadesIncidente
                        {
                            IDPrioridad = (int)datos.Lector["IDPrioridad"],
                            Nombre = (string)datos.Lector["NombrePriori"],
                            Descripcion = (string)datos.Lector["DescripcionPriori"]
                        },

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
        public int AgregarIncidencia(Incidencia nueva)
        {
            AccesoHistorial hist = new AccesoHistorial();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO INCIDENCIAS (IDEMPLEADO, IDCLIENTE, IDTIPO, IDPRIORIDAD, ESTADOACTUAL, DESCRIPCION, FECHAYHORACREACION) OUTPUT INSERTED.IDINCIDENCIA VALUES (@IDEMPLEADO, @IDCLIENTE, @IDTIPO, @IDPRIORIDAD, @ESTADOACTUAL, @DESCRIPCION, @FECHAYHORACREACION)");
                if (nueva.IdEmpleado == 0) datos.SetearParametro("@IDEMPLEADO", DBNull.Value);
                else datos.SetearParametro("@IDEMPLEADO", nueva.IdEmpleado);
                datos.SetearParametro("@IDCLIENTE", nueva.IdCliente);
                datos.SetearParametro("@IDTIPO", nueva.Tipo.IDTipo);
                datos.SetearParametro("@IDPRIORIDAD", nueva.Prioridad.IDPrioridad);
                datos.SetearParametro("@ESTADOACTUAL", nueva.EstadoActual);
                datos.SetearParametro("@DESCRIPCION", nueva.Descripcion);
                datos.SetearParametro("@FECHAYHORACREACION", nueva.FechaYHoraCreacion);
                int id = (int)datos.EjectuarScalar();
                nueva.IdIncidencia = id;
                hist.Agregar(nueva);
                return id;
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public void ModificarIncidencia(Incidencia nueva)
        {
            AccesoHistorial hist = new AccesoHistorial();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Incidencias SET IDEmpleado = @IDEmpleado, IDTipo = @IDTipo, IDPrioridad = @IDPrioridad, EstadoActual = @EstadoActual, FechaYHoraResolucion = @FechaYHoraResolucion, Resolucion = @Resolucion WHERE IDIncidencia = @IDIncidencia");
                if (nueva.IdEmpleado != 0) datos.SetearParametro("@IDEmpleado", nueva.IdEmpleado);
                else datos.SetearParametro("@IDEmpleado", DBNull.Value);
                    datos.SetearParametro("@IDTipo", nueva.Tipo.IDTipo);
                datos.SetearParametro("@IDPrioridad", nueva.Prioridad.IDPrioridad);
                datos.SetearParametro("@EstadoActual", nueva.EstadoActual);
                if (!(string.IsNullOrEmpty(nueva.Resolucion)))
                {
                    datos.SetearParametro("@FechaYHoraResolucion", nueva.FechaYHoraResolucion);
                    datos.SetearParametro("@Resolucion", nueva.Resolucion);
                }
                else
                {
                    datos.SetearParametro("@FechaYHoraResolucion", DBNull.Value);
                    datos.SetearParametro("@Resolucion", DBNull.Value);
                }
                datos.SetearParametro("@IDIncidencia", nueva.IdIncidencia);
                datos.EjecutarNonQuery();
                hist.Agregar(nueva);
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public void ReactivarIncidencia(Incidencia react)
        {
            AccesoHistorial hist = new AccesoHistorial();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Incidencias SET EstadoActual = @EstadoActual, Resolucion = @Resolucion, FechaYHoraResolucion = @FechaYHoraResolucion, IDEmpleado = @IDEmpleado WHERE IDIncidencia = @IDIncidencia");
                datos.SetearParametro("@EstadoActual", "Reactivado");
                datos.SetearParametro("@Resolucion",DBNull.Value);
                datos.SetearParametro("@FechaYHoraResolucion", DBNull.Value);
                datos.SetearParametro("@IDEmpleado", DBNull.Value);
                datos.SetearParametro("@IDIncidencia",react.IdIncidencia);
                datos.EjecutarNonQuery();
                hist.Agregar(react);
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        public bool AsignarIncidencia(int IDIncidencia, int IDEmpleado)
        {
            AccesoHistorial hist = new AccesoHistorial();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE Incidencias SET IDEmpleado = @IDEmpleado WHERE IDIncidencia = @IDIncidencia");
                datos.SetearParametro("@IDEmpleado", IDEmpleado);
                datos.SetearParametro("@IDIncidencia", IDIncidencia);
                datos.EjecutarNonQuery();
                Incidencia inc = Listar().Find(x => x.IdIncidencia == IDIncidencia);
                hist.Agregar(inc);

                return true;
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                datos.Cerrar();
            }
        }

    }
}
