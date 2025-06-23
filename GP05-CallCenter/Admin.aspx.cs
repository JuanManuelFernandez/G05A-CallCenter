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
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    AccesoUsuario datos = new AccesoUsuario();
                    dgvUsuarios.DataSource = datos.ListarAdmin();
                    dgvUsuarios.DataBind();
                    return;
                }
            }
            Response.Redirect("Inicio.aspx");
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("Edicion.aspx?IdUsuario=" + id);
        }
    }
}