using Datos;
using System;
using System.Web.UI;

namespace CallCenter
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    Response.Redirect("Inicio.aspx");
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }
        protected void BtnLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }
    }
}