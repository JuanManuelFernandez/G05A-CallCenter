using System;
using System.Web.UI;

namespace CallCenter
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Cerrar sesión
            Session.Clear(); // Elimina todos los valores de sesión
            Session.Abandon(); // Termina la sesión actual
            Response.Redirect("~/inicio.aspx");
        }
    }
}