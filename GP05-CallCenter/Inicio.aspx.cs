using Datos;
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
        public string NombreDeUsuario{ get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            AccesoClientes data = new AccesoClientes();
            AccesoEmpleados dataEmp = new AccesoEmpleados();
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario == TipoUsuario.Empleado)
            {
                NombreDeUsuario = dataEmp.BuscarPorIdUsuario(user.IdUsuario).Nombre;
                btnMisReclamos.Text = "Reclamos";
            }
            else {
                NombreDeUsuario = (data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).Nombre;
            }
        }

        protected void btnDatos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisDatos.aspx");
        }

        protected void btnReclamos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                Response.Redirect("MisReclamos.aspx");
            }
            else 
            {
                Response.Redirect("Formularios.aspx");
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Incidencias.aspx");
        }
    }
}