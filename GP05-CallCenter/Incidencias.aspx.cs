using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class Incidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null) {
                txtIdCliente.Enabled = false;
                txtDescripcion.Enabled = false;
                txtTelefono.Enabled = false;
                txtMail.Enabled = false;
                txtDireccion.Enabled = false;
                txtTipo.Enabled = false;

                btnCargar.Text = "Aceptar";
                string id = Request.QueryString["id"].ToString();
                AccesoIncidencias datos = new AccesoIncidencias();
                Incidencia actual = datos.Buscar(id);

                lblFechaYHora.Text = actual.FechaYHoraCreacion.ToString();
                txtIdCliente.Text = actual.IdCliente.ToString();
                txtDescripcion.Text = actual.Descripcion.ToString();
                txtTipo.Text = actual.IdTipo.ToString();
                txtEstadoActual.Text = actual.EstadoActual.ToString();
                txtPrioridad.Text = actual.IdPrioridad.ToString();
                txtResolucion.Text= actual.Resolucion.ToString();
            }
        }
    }
}