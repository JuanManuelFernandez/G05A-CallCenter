using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccesoHistorial data = new AccesoHistorial();

            int IDIncidencia = int.Parse(Request.QueryString["id"]);
            dgvHistorial.DataSource = data.ListarPorIDIncidencia(IDIncidencia);

            dgvHistorial.DataBind();
        }
    }
}