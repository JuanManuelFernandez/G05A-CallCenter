using Datos;
using System;

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
                btnIncidenciasLibres.Visible = true;
            }
            else if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                NombreDeUsuario = (data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).Nombre;
            }
            else
            {
                NombreDeUsuario = "Administrador/a";
                btnMisDatos.Text = "Cargar Empleado";
                btnMisReclamos.Text = "Incidencias";
                btnCargar.Text = "Usuarios";
                btnModificarTipos.Visible = true;
            }
        }

        protected void BtnDatos_Click(object sender, EventArgs e)
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

        protected void BtnReclamos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formularios.aspx");
        }

        protected void BtnCargar_Click(object sender, EventArgs e)
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

        protected void BtnModificarTipos_Click(object sender, EventArgs e)
        {
            Response.Redirect("EdicionDatos.aspx");
        }
        protected void BtnDarDeAltaUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }
        protected void BtnIncidenciasLibres_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncidenciasLibres.aspx");

        }
    }
}