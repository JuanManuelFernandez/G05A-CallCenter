using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace CallCenter
{
    public partial class Incidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTipo();
            CargarPrioridad();

            if (Session["usuario"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            else if (Request.QueryString["id"] != null)
            {
                btnCargar.Text = "Modificar";
                btnCargar.CssClass = "btn btn-success btn-lg mx-3";
                CargarIncidencia();
                return;
            }
            else if (((Usuario)Session["usuario"]).TipoUsuario == TipoUsuario.Empleado)
            {
                lblFechaYHora.Text = DateTime.Now.ToString();
                btnCargar.Text = "Cargar";
                btnCargar.CssClass = "btn btn-success btn-lg mx-3";
                txtNombre.Enabled = true;
                txtDNI.Enabled = true;
                txtMail.Enabled = true;
                txtTelefono.Enabled = true;
                return;
            }
            CargarDatosCliente();
        }
        public void CargarIncidencia()
        {
            if (!IsPostBack)
            {
                btnCargar.Text = "Aceptar";
                string id = Request.QueryString["id"].ToString();
                AccesoIncidencias datos = new AccesoIncidencias();
                AccesoClientes datosClientes = new AccesoClientes();
                AccesoEmpleados datosEmpleados = new AccesoEmpleados();
                Incidencia actual = datos.listar().Find(x => x.IdIncidencia == int.Parse(id));
                Cliente cliente = datosClientes.Listar().Find(x => x.IdCliente == actual.IdCliente);
                Empleado empleado = datosEmpleados.listar().Find(x => x.IDEmpleado == actual.IdEmpleado);

                txtDNI.Text = cliente.DNI.ToString();
                txtMail.Text = cliente.Usuario.Email;
                txtTelefono.Text = cliente.Telefono.ToString();
                ddlTipo.SelectedValue = actual.tipo.IDTipo.ToString();
                ddlPrioridad.SelectedValue = actual.prioridad.IDPrioridad.ToString();
                lblFechaYHora.Text = actual.FechaYHoraCreacion.ToString();
                txtEstadoActual.Text = actual.EstadoActual.ToString();
                txtDescripcion.Text = actual.Descripcion;
                txtResolucion.Text = actual.Resolucion;
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            AccesoIncidencias entry = new AccesoIncidencias();
            Usuario user = (Usuario)Session["usuario"];
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                AccesoClientes datos = new AccesoClientes();
                Incidencia nueva = new Incidencia();
                nueva.IdCliente = (datos.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).IdCliente;

                nueva.tipo = new TiposIncidente
                {
                    IDTipo = int.Parse(ddlTipo.SelectedValue)
                };

                nueva.prioridad = new PrioridadesIncidente
                {
                    IDPrioridad = int.Parse(ddlPrioridad.SelectedValue)
                };
                nueva.Descripcion = txtDescripcion.Text;
                entry.AgregarIncidencia(nueva);
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    Incidencia nueva = entry.listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                    nueva.tipo.IDTipo = int.Parse(ddlTipo.SelectedValue);
                    nueva.prioridad.IDPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                    nueva.EstadoActual = txtEstadoActual.Text;
                    if (!(string.IsNullOrEmpty(txtResolucion.Text)))
                    {
                        nueva.FechaYHoraResolucion = DateTime.Now;
                        nueva.Resolucion = txtResolucion.Text;
                    }
                    else
                    {
                        nueva.Resolucion = null;
                    }
                    entry.ModificarIncidencia(nueva);
                }
            }
            Response.Redirect("Inicio.aspx");
        }
        public void CargarTipo()
        {
            if (!IsPostBack)
            {
                AccesoTipos datos = new AccesoTipos();
                try
                {
                    ddlTipo.DataSource = datos.Listar();
                    ddlTipo.DataValueField = "IDTipo";
                    ddlTipo.DataTextField = "Nombre";
                    ddlTipo.DataBind();
                }
                catch (Exception er)
                {

                    throw er;
                }
            }
        }
        public void CargarPrioridad()
        {
            if (!IsPostBack)
            {
                AccesoPrioridades datos = new AccesoPrioridades();
                try
                {
                    ddlPrioridad.DataSource = datos.Listar();
                    ddlPrioridad.DataValueField = "IDPrioridad";
                    ddlPrioridad.DataTextField = "Nombre";
                    ddlPrioridad.DataBind();
                }
                catch (Exception er)
                {

                    throw er;
                }
            }
        }
        protected void txtResolucion_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResolucion.Text))
            {
                btnCargar.Text = "Cerrar";
                btnCargar.CssClass = "btn btn-danger btn-lg mx-3";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
        public void CargarDatosCliente()
        {
            AccesoClientes datos = new AccesoClientes();
            Usuario user = ((Usuario)Session["usuario"]);
            Cliente cli = datos.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
            txtResolucion.Visible = false;
            lblResolucion.Visible = false;
            lblEstadoActual.Visible = false;
            lblPrioridad.Visible = false;
            txtEstadoActual.Visible = false;
            ddlPrioridad.Visible = false;

            lblFechaYHora.Text = DateTime.Now.ToString();
            txtNombre.Text = string.Concat(cli.Apellido, ", ", cli.Nombre);
            txtMail.Text = cli.Usuario.Email;
            txtDNI.Text = cli.DNI.ToString();
            txtTelefono.Text = cli.Telefono.ToString();
        }
    }
}