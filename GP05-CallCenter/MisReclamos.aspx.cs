using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class MisReclamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session.Add("error", "Debes loguearte para ver esta pagina");
                Response.Redirect("Error.aspx");
                return;
            }
            Usuario user = (Usuario)Session["usuario"];
            AccesoClientes dataCli = new AccesoClientes();
            Cliente cli = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
            AccesoIncidencias data = new AccesoIncidencias();
            gdvReclamos.DataSource = data.listarReclamos(cli.IdCliente);
            gdvReclamos.DataBind();
        }
    }
}