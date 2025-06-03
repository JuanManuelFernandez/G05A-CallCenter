using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector = null;

        public SqlDataReader Lector {
            get { return lector; }
        }
        public void Conectar() {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TPI_CallCenter; integrated security=true");
            comando = new SqlCommand();
        }
        public void Consultar(string consulta) {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void Leer() { 
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception er)
            {

                throw er;
            }
        }
        public void EjecutarNonQuery() {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception er)
            {

                throw er;
            }
        }
        public void Cerrar()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
        public void setearParametro(string columna, object dato)
        {
            comando.Parameters.AddWithValue(columna, dato);
        }
    }
}
