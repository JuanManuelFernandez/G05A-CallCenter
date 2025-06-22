using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoCategorias
    {
        private AccesoDatos datos = null;
        private List<CategoriasCliente> categorias = null;
        public List<CategoriasCliente> Listar() { 
            datos = new AccesoDatos();
            categorias = new List<CategoriasCliente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDCategoria, Nombre, Descripcion FROM CategoriasCliente");
                datos.Leer();

                while (datos.Lector.Read()) { 
                    CategoriasCliente aux = new CategoriasCliente();
                    aux.IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
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
    }
}
