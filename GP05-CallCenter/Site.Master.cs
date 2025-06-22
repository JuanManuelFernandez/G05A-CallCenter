using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            Response.Redirect("~/Inicio.aspx");
        }
    }
}