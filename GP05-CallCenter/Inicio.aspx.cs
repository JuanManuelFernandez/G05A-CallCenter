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
            var data = new AccesoClientes();
            var dataEmp = new AccesoEmpleados();
            var user = (Usuario)Session["usuario"];
            switch (user.TipoUsuario)
            {
                // Empleado
                case TipoUsuario.Empleado:
                    NombreDeUsuario = dataEmp.BuscarPorIdUsuario(user.IdUsuario).Nombre;
                    btnMisReclamos.Text = "reclamos";
                    btnRegistrarCliente.Visible = true;
                    btnIncidenciasLibres.Visible = true;
                    break;
                // Cliente
                case TipoUsuario.Cliente:
                    NombreDeUsuario = (data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).Nombre;
                    break;
                // admin
                default:
                    NombreDeUsuario = "Administrador/a";
                    btnMisDatos.Text = "Cargar Empleado";
                    btnMisReclamos.Text = "incidencia";
                    btnCargar.Text = "Usuarios";
                    btnModificarTipos.Visible = true;
                    btnDiarios.Visible = true;
                    break;
            }
        }
        protected void BtnDatos_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["usuario"];
            Response.Redirect(user.TipoUsuario == TipoUsuario.Admin ? "registro.aspx" : "misdatos.aspx");
        }
        protected void BtnReclamos_Click(object sender, EventArgs e)
        {
            Response.Redirect("reclamos.aspx");
        }
        protected void BtnCargar_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["usuario"];
            Response.Redirect(user.TipoUsuario != TipoUsuario.Admin ? "incidencia.aspx" : "admin.aspx");
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