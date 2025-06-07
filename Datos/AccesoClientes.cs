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
        private List<Clientes> clientes = null;
        private AccesoDatos datos = null;

        public List<Clientes> Listar()
        {
            clientes = new List<Clientes>();
            datos = new AccesoDatos();

            datos.Conectar();
            datos.Consultar("SELECT * FROM Clientes");
            datos.Leer();

            try
            {
                while (datos.Lector.Read())
                {
                    Clientes aux = new Clientes();
                    aux.IdCliente = (int)datos.Lector["IDCliente"]; // IDPersona cambiado por IDCliente
                    aux.Dni = (string)datos.Lector["DNI"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Mail"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Contraseña = (string)datos.Lector["Contraseña"];
                    aux.IdCategoria = (int)datos.Lector["IDCategoria"]; // Deberia leer de la tabla Categorias

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
        public void AgregarCliente(Clientes nuevo)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Clientes (DNI, Nombre, Apellido, Mail, Telefono, Contraseña) VALUES (@DNI, @Nombre, @Apellido, @Mail, @Telefono, @Contraseña)");
                datos.setearParametro("@DNI", nuevo.Dni);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Mail", nuevo.Email);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Contraseña", nuevo.Contraseña);
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
