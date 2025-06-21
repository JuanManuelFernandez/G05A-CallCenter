using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                    Response.Redirect("Admin.aspx");
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
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }
    }
}