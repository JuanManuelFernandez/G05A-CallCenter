using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            AccesoUsuario datos = new AccesoUsuario();
            List<Usuario> lista = datos.Listar();
            foreach (Usuario aux in lista) {
                if (aux.Email == txtMail.Text && aux.Clave == txtContraseña.Text) {
                    Response.Redirect("Formularios.aspx");
                }
            }
            lblError.Visible = true;
        }
    }
}