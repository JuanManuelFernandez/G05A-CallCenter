using Datos;
using System;

namespace CallCenter
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblConfirmar.Visible = false;
            lblReconfirmar.Visible = false;
            lblError.Visible = false;
            txtPass.Visible = false;
            txtRePass.Visible = false;
            btnConfirmar.Visible = false;
            btnConfirmarCambio.Visible = false;
            if (Session["usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void BtnMod_Click(object sender, EventArgs e)
        {
            lblConfirmar.Visible = true;
            lblConfirmar.Text = "Ingrese su nueva contraseña:";
            txtPass.Visible = true;
            lblReconfirmar.Visible = true;
            lblReconfirmar.Text = "Ingrese su nueva contraseña otra vez:";
            txtRePass.Visible = true;
            btnConfirmarCambio.Visible = true;
        }
        protected void BntDel_Click(object sender, EventArgs e)
        {
            lblConfirmar.Text = "Seguro que desea eliminar su cuenta?";
            lblConfirmar.Visible = true;
            btnConfirmar.Visible = true;

        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            var data = new AccesoUsuario();
            var user = (Usuario)Session["usuario"];
            data.EliminarUsuarioId(user.IdUsuario);
            Session.Clear();
            Response.Redirect("landing.aspx");
        }
        protected void btnConfirmarCambio_Click(object sender, EventArgs e)
        {
            var data = new AccesoUsuario();
            var user = (Usuario)Session["usuario"];

            if (txtPass.Text != null)
            {
                if (txtPass.Text == txtRePass.Text)
                {
                    user.Clave = txtPass.Text;
                    data.ModificarUsuario(user);
                    Session.Clear();
                    Response.Redirect("landing.aspx");
                }
                else
                {
                    txtPass.Text = "";
                    txtRePass.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "Contraseñas no coinciden.";
                }
            }
        }
    }
}