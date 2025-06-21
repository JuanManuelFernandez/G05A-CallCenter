using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDatos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisDatos.aspx");
        }

        protected void btnReclamos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisReclamos.aspx");
        }
    }
}