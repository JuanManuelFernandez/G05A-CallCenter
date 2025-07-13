using Dominio;
using System;
using System.Collections.Generic;

namespace Datos
{
    public class AccesoCategorias
    {
        private AccesoDatos datos = null;
        private List<CategoriasCliente> categorias = null;
        public List<CategoriasCliente> Listar()
        {
            datos = new AccesoDatos();
            categorias = new List<CategoriasCliente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCategoria, Nombre, Descripcion FROM CategoriasCliente WHERE Eliminado = 0");
                datos.Leer();

                while (datos.Lector.Read())
                {
                    CategoriasCliente aux = new CategoriasCliente
                    {
                        IdCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                    categorias.Add(aux);
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
            return categorias;
        }
        public List<CategoriasCliente> ListarCategoriasEliminadas()
        {
            datos = new AccesoDatos();
            categorias = new List<CategoriasCliente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCategoria, Nombre, Descripcion FROM CategoriasCliente WHERE Eliminado = 1");
                datos.Leer();

                while (datos.Lector.Read())
                {
                    CategoriasCliente aux = new CategoriasCliente
                    {
                        IdCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty
                    };
                    categorias.Add(aux);
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
            return categorias;
        }
        public void AgregarCategoria(CategoriasCliente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO CategoriasCliente(Nombre, Descripcion) VALUES (@Nombre, @Descripcion)");
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
        public void EliminarCategoria(CategoriasCliente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE CategoriasCliente SET Eliminado = 1 WHERE IDCategoria = @IDCategoria");
                datos.SetearParametro("@IDCategoria", nuevo.IdCategoria);
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
        public void ReactivarCategoria(CategoriasCliente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE CategoriasCliente SET Eliminado = 0 WHERE IDCategoria = @IDCategoria");
                datos.SetearParametro("@IDCategoria", nuevo.IdCategoria);
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
        public void ModificarCategoria(CategoriasCliente nuevo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("UPDATE CategoriasCliente SET Nombre = @Nombre, Descripcion = @Descripcion WHERE IDCategoria = @IDCategoria");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@IDCategoria", nuevo.IdCategoria);
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
