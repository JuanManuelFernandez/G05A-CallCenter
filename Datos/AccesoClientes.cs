using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoClientes
    {
        private List<Cliente> clientes = null;
        private AccesoDatos datos = null;

        public List<Cliente> Listar()
        {
            clientes = new List<Cliente>();
            datos = new AccesoDatos();

            // Unimos Clientes y Usuarios mediante IDUsuario
            datos.Conectar();
            datos.Consultar("SELECT C.IDCliente,IDCategoria,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Email,U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente
                    {
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        Categoria = new CategoriasCliente
                        {
                            IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0,
                        },
                        Usuario = new Usuario
                        {
                            IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                            TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                            Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                            Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty
                        },
                        DNI = datos.Lector["DNI"] != DBNull.Value ? (int)datos.Lector["DNI"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty
                    };

                    clientes.Add(aux);
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
            return clientes;
        }
        public void AgregarCliente(Cliente nuevo)
        {
            datos = new AccesoDatos();
            AccesoUsuario auxiliar = new AccesoUsuario();

            try
            {
                // Insertar en Usuarios
                datos.Conectar();
                datos.Consultar("INSERT INTO Usuarios (TipoUsuario, Email, Clave, Eliminado) VALUES (@TipoUsuario, @Email, @Clave, @Eliminado)");
                datos.setearParametro("@TipoUsuario", nuevo.Usuario.TipoUsuario);
                datos.setearParametro("@Clave", nuevo.Usuario.Clave);
                datos.setearParametro("@Email", nuevo.Usuario.Email);
                datos.setearParametro("@Eliminado", 0);
                datos.EjecutarNonQuery();
                datos.Cerrar();

                // Insertar en Clientes usando el IDUsuario recién generado
                datos.Conectar();
                datos.Consultar("INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES (@IDUsuario, @DNI, @Nombre, @Apellido, @Telefono)");
                datos.setearParametro("@IDUsuario", auxiliar.Listar()[(auxiliar.Listar().Count) - 1].IdUsuario);
                datos.setearParametro("@DNI", nuevo.DNI);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Telefono", nuevo.Telefono);

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
        public Cliente BuscarClientePorIdUsuario(int id)
        {
            datos = new AccesoDatos();
            Cliente aux;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT C.IDCliente,IDCategoria,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.TipoUsuario,U.Email,U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE U.IDUsuario = " + id);
                datos.Leer();
                datos.Lector.Read();
                aux = new Cliente();

                aux.IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0;
                aux.Categoria = new CategoriasCliente();
                aux.Categoria.IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0;
                aux.Usuario = new Usuario();
                aux.Usuario.IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0;
                aux.Usuario.TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente;
                aux.Usuario.Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty;
                aux.Usuario.Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty;
                aux.DNI = datos.Lector["DNI"] != DBNull.Value ? (int)datos.Lector["DNI"] : 0;
                aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;
                aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty;

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
        public bool BuscarClientePorEmail(string email)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT Email, Eliminado FROM Usuarios WHERE Email =" + email);
                datos.Leer();
                datos.Lector.Read();

                if (datos.Lector["Email"] != DBNull.Value)
                {
                    return true;
                }
                return false;
            }
            finally
            {
                datos.Cerrar();
            }
        }
        //REVISAR ESTA FUNCION
        public Cliente BuscarClientePorDNI(int dni)
        {
            datos = new AccesoDatos();
            Cliente aux = null;
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT C.IDCliente, IDCategoria, C.IDUsuario, C.DNI, C.Nombre, C.Apellido, C.Telefono, U.TipoUsuario, U.Email, U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario WHERE C.DNI = @Dni");
                datos.setearParametro("@Dni", dni);
                datos.Leer();

                if (datos.Lector.Read())
                {
                    aux = new Cliente
                    {
                        IdCliente = datos.Lector["IDCliente"] != DBNull.Value ? (int)datos.Lector["IDCliente"] : 0,
                        Categoria = new CategoriasCliente
                        {
                            IDCategoria = datos.Lector["IDCategoria"] != DBNull.Value ? (int)datos.Lector["IDCategoria"] : 0,
                        },
                        Usuario = new Usuario
                        {
                            IdUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0,
                            TipoUsuario = datos.Lector["TipoUsuario"] != DBNull.Value ? (TipoUsuario)datos.Lector["TipoUsuario"] : TipoUsuario.Cliente,
                            Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : string.Empty,
                            Clave = datos.Lector["Clave"] != DBNull.Value ? (string)datos.Lector["Clave"] : string.Empty
                        },
                        DNI = datos.Lector["DNI"] != DBNull.Value ? (int)datos.Lector["DNI"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty,
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty
                    };
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

        public void ModificarCliente(Cliente mod)
        {
            datos = new AccesoDatos();
            AccesoUsuario dataUsuarios = new AccesoUsuario();
            try
            {
                datos.Conectar();
                dataUsuarios.ModificarUsuario(mod.Usuario);
                datos.Cerrar();

                datos.Conectar();
                datos.Consultar("UPDATE Clientes SET IDCategoria = @IDCategoria, DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE IDUsuario = @IDUsuario");
                if (mod.Categoria == null || mod.Categoria.IDCategoria == 0)
                {
                    datos.setearParametro("@IDCategoria", DBNull.Value);
                }
                else
                {
                    datos.setearParametro("@IDCategoria", mod.Categoria.IDCategoria);
                }
                datos.setearParametro("@DNI", mod.DNI);
                datos.setearParametro("@Nombre", mod.Nombre);
                datos.setearParametro("@Apellido", mod.Apellido);
                datos.setearParametro("@Telefono", mod.Telefono);
                datos.setearParametro("@IDUsuario", mod.Usuario.IdUsuario);
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