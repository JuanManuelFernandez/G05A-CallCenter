using System;
using System.Collections.Generic;
using Dominio;

namespace Datos
{
    public class AccesoPlantillas
    {
        private AccesoDatos datos = null;
        private List<Plantilla> plantillas = null;
        public List<Plantilla> Listar()
        {
            datos = new AccesoDatos();
            plantillas = new List<Plantilla>();

            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDPlantilla, Nombre, Descripcion FROM Plantillas WHERE Eliminado = 0");
                datos.Leer();

                while (datos.Lector.Read())
                {
                    var aux = new Plantilla
                    {
                        IdPlantilla = datos.Lector["IDPlantilla"] != DBNull.Value ? (int)datos.Lector["IDPlantilla"] : 0,
                        Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty,
                        Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty,
                    };
                    plantillas.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
            return plantillas;
        }
        public Plantilla Buscar(int id)
        {
            datos = new AccesoDatos();
            var aux = new Plantilla();
            try
            {
                datos.Conectar();
                datos.Consultar("SELECT IDPlantilla, Nombre, Descripcion FROM Plantillas WHERE IDPlantilla = " + id);
                datos.Leer();
                aux.IdPlantilla = (int)datos.Lector["IDPlantilla"];
                aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : string.Empty;
                aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : string.Empty;
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
    }
}
