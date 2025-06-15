using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

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

            datos.Conectar();
            // Unimos Clientes y Usuarios mediante IDUsuario
            datos.Consultar("SELECT C.IDCliente,C.IDCategoria,C.IDUsuario,C.DNI,C.Nombre,C.Apellido,C.Telefono,U.Email,U.Clave FROM Clientes C INNER JOIN Usuarios U ON C.IDUsuario = U.IDUsuario");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente
                    {
                        IdCliente = (int)datos.Lector["IDCliente"],
                        IdCategoria = (int)datos.Lector["IDCategoria"],// Deberia leer de la tabla Categorias
                        Usuario = new Usuario
                        {
                            Email = (string)datos.Lector["U.Email"],
                            Clave = (string)datos.Lector["U.Clave"]
                        },
                        DNI = (string)datos.Lector["DNI"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Telefono = (string)datos.Lector["Telefono"]
                    };
                    // Ahora se referencian desde Usuarios
                    //aux.Email = (string)datos.Lector["U.Email"];
                    //aux.Clave = (string)datos.Lector["U.Clave"];

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

            try
            {
                // Insertar en Usuarios
                datos.Consultar("INSERT INTO Usuarios (Email, Clave) VALUES (@Email, @Clave)");
                datos.setearParametro("@Email", nuevo.Usuario.Email);
                datos.setearParametro("@Clave", nuevo.Usuario.Clave);

                // Insertar en Clientes usando el IDUsuario recién generado
                datos.Consultar("INSERT INTO Clientes (IDCategoria, IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES (@IDCateg, @IDUsuario, @DNI, @Nombre, @Apellido, @Telefono)");
                datos.setearParametro("@IDCateg", nuevo.IdCategoria);
                datos.setearParametro("@IDUsuario", nuevo.Usuario.IdUsuario);
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
    }
}
