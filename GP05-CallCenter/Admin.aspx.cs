using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccesoUsuario datos = new AccesoUsuario();
            dgvUsuarios.DataSource = datos.Listar();
            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("Edicion.aspx?IdUsuario=" + id);
        }
    }
}