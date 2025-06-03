using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace GP05_CallCenter
{
    public partial class Incidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null) {
                string id = Request.QueryString["id"].ToString();
                AccesoIncidencias datos = new AccesoIncidencias();
                Incidencias actual = datos.Buscar(id);

                lblFechaYHora.Text = actual.FechaYHora.ToString();
                txtDNI.Text = actual.DNI.ToString();
                txtDescripcion.Text = actual.Descripcion.ToString();
            }
        }
    }
}