using Datos;
using System;

namespace CallCenter
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                var user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    var datos = new AccesoUsuario();
                    dgvUsuarios.DataSource = datos.ListarUsuarios();
                    dgvUsuarios.DataBind();
                    return;
                }
            }
            Response.Redirect("inicio.aspx");
        }

        protected void DgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("editarcuenta.aspx?IdUsuario=" + id);
        }
    }
}