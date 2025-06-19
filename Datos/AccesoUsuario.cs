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
                datos.Consultar("SELECT IDUsuario, TipoUsuario, Email, Clave FROM Usuarios");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario
                    {
                        IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                        TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : 0,
                        Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                        Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty
                    };

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
                datos.Consultar("INSERT INTO Usuarios (TipoUsuario, Email, Clave) VALUES (@TipoUsuario, @Email, @Clave)");
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
        public bool Loguear(Usuario usuario) {
            datos = new AccesoDatos();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IdUsuario, TipoUsuario, Email, Clave FROM Usuarios WHERE Email = @Email AND Clave = @Clave");
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Clave", usuario.Clave);

                //datos.EjecutarNonQuery();
                //datos.Cerrar();
                datos.Leer();
                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)datos.Lector["TipoUsuario"] == 1 ? TipoUsuario.ADMIN : (int)datos.Lector["TipoUsuario"] == 2 ? TipoUsuario.EMPLEADO : TipoUsuario.CLIENTE;
                    //datos.Cerrar();
                    return true;
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
            return false;
        }
    }
}
