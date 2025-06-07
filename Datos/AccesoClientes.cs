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
                    aux.id = (int)datos.Lector["IDPersona"];
                    aux.dni = (string)datos.Lector["DNI"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.apellido = (string)datos.Lector["Apellido"];
                    aux.email = (string)datos.Lector["Mail"];
                    aux.telefono = (string)datos.Lector["Telefono"];
                    aux.contraseña = (string)datos.Lector["Contraseña"];

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
                datos.Consultar("INSERT INTO Cliente (DNI, Nombre, Apellido, Mail, Telefono, Contraseña) VALUES (@DNI, @Nombre, @Apellido, @Mail, @Telefono, @Contraseña)");
                datos.setearParametro("@DNI", nuevo.dni);
                datos.setearParametro("@Nombre", nuevo.nombre);
                datos.setearParametro("@Apellido", nuevo.apellido);
                datos.setearParametro("@Mail", nuevo.email);
                datos.setearParametro("@Telefono", nuevo.telefono);
                datos.setearParametro("@Contraseña", nuevo.contraseña);
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
