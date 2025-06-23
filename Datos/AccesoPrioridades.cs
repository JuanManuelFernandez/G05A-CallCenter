using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoPrioridades
    {
        private AccesoDatos datos = null;
        private List<PrioridadesIncidente> prioridades = null;
        public List<PrioridadesIncidente> Listar()
        {
            datos = new AccesoDatos();
            prioridades = new List<PrioridadesIncidente>();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDPrioridad, Nombre, Descripcion FROM PrioridadesIncidente");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    PrioridadesIncidente aux = new PrioridadesIncidente();
                    aux.IDPrioridad = datos.Lector["IDPrioridad"] != DBNull.Value ? (int)datos.Lector["IDPrioridad"] : 0;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
                    prioridades.Add(aux);
                }
            }
            catch (Exception er)
            {

                throw er;
            }
            return prioridades;
        }
        public PrioridadesIncidente BuscarPorId(int id)
        {
            datos = new AccesoDatos();
            PrioridadesIncidente valor = new PrioridadesIncidente();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT Nombre, Descripcion FROM PrioridadesIncidente WHERE IDPrioridad=" + id);
                datos.Leer();
                datos.Lector.Read();
                valor.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                valor.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
            }
            catch (Exception er)
            {

                throw er;
            }
            return valor;
        }

    }
}
