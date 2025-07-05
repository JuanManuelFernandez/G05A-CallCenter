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
        public string NombreDeUsuario { get; set; }
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
                btnRegistrarCliente.Visible = true;
            }
            else if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                NombreDeUsuario = (data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).Nombre;
            }
            else
            {
                NombreDeUsuario = "Administrador/a";
                btnMisDatos.Text = "Cargar Empleado";
                btnMisReclamos.Text = "Administrar Incidencias";
                btnCargar.Text = "Administrar Usuarios";
                btnModificarTipos.Visible = true;
            }
        }

        protected void btnDatos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario == TipoUsuario.Admin)
            {
                Response.Redirect("Registro.aspx");
            }
            else
            {
                Response.Redirect("MisDatos.aspx");
            }
        }

        protected void btnReclamos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formularios.aspx");
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario != TipoUsuario.Admin)
            {
                Response.Redirect("Incidencias.aspx");
            }
            else
            {
                Response.Redirect("Admin.aspx");
            }
        }

        protected void btnModificarTipos_Click(object sender, EventArgs e)
        {
            Response.Redirect("EdicionDatos.aspx");
        }
        protected void btnDarDeAltaUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }
    }
}