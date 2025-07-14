using Datos;
using System;

namespace CallCenter
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data = new AccesoHistorial();

            var idIncidencia = int.Parse(Request.QueryString["id"]);
            dgvHistorial.DataSource = data.ListarPorIdIncidencia(idIncidencia);

            dgvHistorial.DataBind();
        }
    }
}