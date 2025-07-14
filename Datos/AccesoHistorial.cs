using Dominio;
using System;
using System.Collections.Generic;

namespace Datos
{
    public class AccesoHistorial
    {
        private List<Historial> historial = null;
        private AccesoDatos datos = null;
        public List<Historial> Listar()
        {
            //AccesoPrioridades priori = new AccesoPrioridades();
            historial = new List<Historial>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT IDHistorial, IDIncidencia, EstadoActual, Descripcion FROM Historial");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Historial
                    {
                        IdHistorial = datos.Lector["IDHistorial"] != DBNull.Value ? (int)datos.Lector["IDHistorial"] : 0,
                        IdIncidencia = datos.Lector["IDIncidencia"] != DBNull.Value ? (int)datos.Lector["IDIncidencia"] : 0,
                        EstadoActual = datos.Lector["EstadoActual"] != DBNull.Value ? (string)datos.Lector["EstadoActual"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                    historial.Add(aux);
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
            return historial;
        }
        public List<Historial> ListarPorIdIncidencia(int idIncidencia)
        {
            //AccesoPrioridades priori = new AccesoPrioridades();
            historial = new List<Historial>();
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("SELECT IDHistorial, IDIncidencia, EstadoActual, Descripcion FROM Historial WHERE IDIncidencia = " + idIncidencia);
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    var aux = new Historial
                    {
                        IdHistorial = datos.Lector["IDHistorial"] != DBNull.Value ? (int)datos.Lector["IDHistorial"] : 0,
                        IdIncidencia = datos.Lector["IDIncidencia"] != DBNull.Value ? (int)datos.Lector["IDIncidencia"] : 0,
                        EstadoActual = datos.Lector["EstadoActual"] != DBNull.Value ? (string)datos.Lector["EstadoActual"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                    };
                    historial.Add(aux);
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
            return historial;
        }
        public void Agregar(Incidencias nueva)
        {
            datos = new AccesoDatos();
            datos.Conectar();
            datos.Consultar("INSERT INTO Historial (IDIncidencia, EstadoActual, Descripcion) VALUES (@IDIncidencia, @EstadoActual, @Descripcion)");
            try
            {
                datos.SetearParametro("@IDincidencia", nueva.IdIncidencia);
                datos.SetearParametro("@EstadoActual", nueva.EstadoActual);
                datos.SetearParametro("@Descripcion", nueva.Descripcion);
                datos.EjecutarNonQuery();
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
