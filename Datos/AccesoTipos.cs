using Dominio;
using System;
using System.Collections.Generic;

namespace Datos
{
    public class AccesoTipos
    {
        private AccesoDatos datos = null;
        private List<TiposIncidente> tipos = null;
        public List<TiposIncidente> Listar()
        {
            datos = new AccesoDatos();
            tipos = new List<TiposIncidente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDTipo, Nombre, Descripcion FROM TiposIncidente WHERE Eliminado = 0");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    TiposIncidente aux = new TiposIncidente
                    {
                        IdTipo = datos.Lector["IDTipo"] != DBNull.Value ? (int)datos.Lector["IDTipo"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                    tipos.Add(aux);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return tipos;
        }
        public List<TiposIncidente> ListarTiposEliminados()
        {
            datos = new AccesoDatos();
            tipos = new List<TiposIncidente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDTipo, Nombre, Descripcion FROM TiposIncidente WHERE Eliminado = 1");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    TiposIncidente aux = new TiposIncidente
                    {
                        IdTipo = datos.Lector["IDTipo"] != DBNull.Value ? (int)datos.Lector["IDTipo"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                    tipos.Add(aux);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return tipos;
        }
        public void AgregarTipos(TiposIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO TiposIncidente(Nombre, Descripcion) VALUES (@Nombre, @Descripcion)");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
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
        public void EliminarTipos(TiposIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE TiposIncidente SET Eliminado = 1 WHERE IDTipo = @IDTipo");
                datos.SetearParametro("@IDTipo", nuevo.IdTipo);
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
        public void ReactivarTipos(TiposIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE TiposIncidente SET Eliminado = 0 WHERE IDTipo = @IDTipo");
                datos.SetearParametro("@IDTipo", nuevo.IdTipo);
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
        public void ModificarTipos(TiposIncidente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE TiposIncidente SET Nombre = @Nombre, Descripcion = @Descripcion WHERE IDTipo = @IDTipo");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@IDTipo", nuevo.IdTipo);
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
