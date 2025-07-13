using Datos;
using System;

namespace CallCenter
{
    public partial class Inicio : System.Web.UI.Page
    {
        public string NombreDeUsuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("landing.aspx");
            }
            AccesoClientes data = new AccesoClientes();
            AccesoEmpleados dataEmp = new AccesoEmpleados();
            Usuario user = (Usuario)Session["usuario"];
            // Empleado
            if (user.TipoUsuario == TipoUsuario.Empleado)
            {
                NombreDeUsuario = dataEmp.BuscarPorIdUsuario(user.IdUsuario).Nombre;
                btnMisReclamos.Text = "reclamos";
                btnRegistrarCliente.Visible = true;
                btnIncidenciasLibres.Visible = true;
            }
            // Cliente
            else if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                NombreDeUsuario = (data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).Nombre;
            }
            // admin
            else
            {
                NombreDeUsuario = "Administrador/a";
                btnMisDatos.Text = "Cargar Empleado";
                btnMisReclamos.Text = "incidencia";
                btnCargar.Text = "Usuarios";
                btnModificarTipos.Visible = true;
                btnDiarios.Visible = true;
            }
        }
        protected void BtnDatos_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario == TipoUsuario.Admin)
            {
                Response.Redirect("registro.aspx");
            }
            else
            {
                Response.Redirect("misdatos.aspx");
            }
        }
        protected void BtnReclamos_Click(object sender, EventArgs e)
        {
            Response.Redirect("reclamos.aspx");
        }
        protected void BtnCargar_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario != TipoUsuario.Admin)
            {
                Response.Redirect("incidencia.aspx");
            }
            else
            {
                Response.Redirect("admin.aspx");
            }
        }
        protected void BtnModificarTipos_Click(object sender, EventArgs e)
        {
            Response.Redirect("editardatos.aspx");
        }
        protected void BtnDarDeAltaUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("registro.aspx");
        }
        
        protected void BtnDiarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("diarios.aspx");
        }

        protected void BtnIncidenciasLibres_Click(object sender, EventArgs e)
        {
            Response.Redirect("incidenciaslibres.aspx");

        }
    }
}