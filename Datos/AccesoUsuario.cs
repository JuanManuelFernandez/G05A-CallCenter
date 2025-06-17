using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoUsuario
    {
        private List<Usuario> usuarios = null;
        private AccesoDatos datos = null;

        public List<Usuario> Listar() {
            usuarios = new List<Usuario>();
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDUsuario, Email, Clave FROM Usuarios");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0;
                    aux.Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty;
                    aux.Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty;

                    usuarios.Add(aux);
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
            return usuarios;
        }

        public void AgregarUsuario(Usuario nuevo) {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Usuarios (Email, Clave) VALUES (@Email, @Clave)");
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Clave", nuevo.Clave);
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
