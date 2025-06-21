using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoEmpleados
    {
        AccesoDatos datos;
        List<Empleado> empleados;
        public List<Empleado> listar()
        {
            datos = new AccesoDatos();
            empleados = new List<Empleado>();

            datos.Conectar();
            datos.Consultar("SELECT IDEmpleado, IDUsuario, Legajo, DNI, Nombre, Apellido FROM Empleados");
            datos.Leer();
            try
            {
                while (datos.Lector.Read())
                {
                    Empleado aux = new Empleado();
                    aux.IDEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0;
                    aux.IDUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0;
                    aux.Legajo = datos.Lector["Legajo"] != DBNull.Value ? (string)datos.Lector["Legajo"] : string.Empty;
                    aux.DNI = datos.Lector["DNI"] != DBNull.Value ? (int)datos.Lector["DNI"] : 0;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;
                    empleados.Add(aux);
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
            return empleados;
        }
        public Empleado BuscarPorIdUsuario(int id) {
            datos = new AccesoDatos();
            empleados = new List<Empleado>();
            Empleado aux = new Empleado();

            datos.Conectar();
            datos.Consultar("SELECT IDEmpleado, IDUsuario, Legajo, DNI, Nombre, Apellido FROM Empleados WHERE IDUsuario = " + id);
            datos.Leer();
            datos.Lector.Read();
            aux.IDEmpleado = datos.Lector["IDEmpleado"] != DBNull.Value ? (int)datos.Lector["IDEmpleado"] : 0;
            aux.IDUsuario = datos.Lector["IDUsuario"] != DBNull.Value ? (int)datos.Lector["IDUsuario"] : 0;
            aux.Legajo = datos.Lector["Legajo"] != DBNull.Value ? (string)datos.Lector["Legajo"] : string.Empty;
            aux.DNI = datos.Lector["DNI"] != DBNull.Value ? (int)datos.Lector["DNI"] : 0;
            aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
            aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : string.Empty;

            return aux;
        }
    }
}
