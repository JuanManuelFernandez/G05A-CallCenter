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
                txtDNI.Enabled = false;
                txtDescripcion.Enabled = false;
                txtTelefono.Enabled = false;
                txtMail.Enabled = false;
                txtDireccion.Enabled = false;
                txtTipo.Enabled = false;

                btnCargar.Text = "Aceptar";
                string id = Request.QueryString["id"].ToString();
                AccesoIncidencias datos = new AccesoIncidencias();
                Incidencias actual = datos.Buscar(id);

                lblFechaYHora.Text = actual.FechaYHoraCreacion.ToString();
                txtDNI.Text = actual.DNI.ToString();
                txtDescripcion.Text = actual.Descripcion.ToString();
                txtTipo.Text = actual.IdTipo.ToString();
                txtEstado.Text = actual.Estado.ToString();
                txtPrioridad.Text = actual.IdPrioridad.ToString();
                txtResolucion.Text= actual.Resolucion.ToString();
            }
        }
    }
}