using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoPrioridades
    {
        private AccesoDatos datos = null;
        private List<PrioridadesIncidente> prioridades = null;
        public List<PrioridadesIncidente> Listar()
        {
            datos = new AccesoDatos();
            prioridades = new List<PrioridadesIncidente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDPrioridad, Nombre, Descripcion FROM PrioridadesIncidente WHERE Eliminado = 0");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    PrioridadesIncidente aux = new PrioridadesIncidente();
                    aux.IDPrioridad = datos.Lector["IDPrioridad"] != DBNull.Value ? (int)datos.Lector["IDPrioridad"] : 0;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    prioridades.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            return prioridades;
        }
        public List<PrioridadesIncidente> ListarPrioridadesEliminadas()
        {
            datos = new AccesoDatos();
            prioridades = new List<PrioridadesIncidente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDPrioridad, Nombre, Descripcion FROM PrioridadesIncidente WHERE Eliminado = 1");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    PrioridadesIncidente aux = new PrioridadesIncidente();
                    aux.IDPrioridad = datos.Lector["IDPrioridad"] != DBNull.Value ? (int)datos.Lector["IDPrioridad"] : 0;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    prioridades.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            return prioridades;
        }
        public PrioridadesIncidente BuscarPorId(int id)
        {
            datos = new AccesoDatos();
            PrioridadesIncidente valor = new PrioridadesIncidente();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT Nombre, Descripcion FROM PrioridadesIncidente WHERE IDPrioridad=" + id);
                datos.Leer();
                datos.Lector.Read();
                valor.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                valor.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
            }
            catch (Exception er)
            {

                throw er;
            }
            return valor;
        }
        public void AgregarPrioridades(PrioridadesIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO PrioridadesIncidente(Nombre, Descripcion) VALUES (@Nombre, @Descripcion)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
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
        public void EliminarPrioridades(PrioridadesIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE PrioridadesIncidente SET Eliminado = 1 WHERE IDPrioridad = @IDPrioridad");
                datos.setearParametro("@IDPrioridad", nuevo.IDPrioridad);
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
        public void ReactivarPrioridades(PrioridadesIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE PrioridadesIncidente SET Eliminado = 0 WHERE IDPrioridad = @IDPrioridad");
                datos.setearParametro("@IDPrioridad", nuevo.IDPrioridad);
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
        public void ModificarPrioridades(PrioridadesIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE PrioridadesIncidente SET Nombre = @Nombre, Descripcion = @Descripcion WHERE IDPrioridad = @IDPrioridad");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IDPrioridad", nuevo.IDPrioridad);
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
