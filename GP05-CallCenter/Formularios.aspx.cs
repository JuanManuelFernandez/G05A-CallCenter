using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class Formularios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session.Add("error", "Debes loguearte para acceder a esta pagina");
            }

            AccesoIncidencias datos = new AccesoIncidencias();
            dgvIncidencias.DataSource = datos.listar();
            dgvIncidencias.DataBind();
        }

        protected void dgvIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvIncidencias.SelectedDataKey.Value.ToString();
            Response.Redirect("Incidencia.aspx?id=" + id);
        }
    }
}