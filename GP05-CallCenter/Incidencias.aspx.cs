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
            CargarCategoria();

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
                txtApellido.Enabled = true;
                txtNombre.Enabled = true;
                txtDNI.Enabled = true;
                txtMail.Enabled = true;
                txtTelefono.Enabled = true;
                ddlCategoria.Enabled = true;
                txtResolucion.Visible = false;
                lblResolucion.Visible = false;
                return;
            }
            CargarDatosCliente();
        }
        public void CargarIncidencia()
        {
            btnCargar.Text = "Aceptar";
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"].ToString();

                AccesoIncidencias datos = new AccesoIncidencias();
                AccesoClientes datosClientes = new AccesoClientes();
                AccesoEmpleados datosEmpleados = new AccesoEmpleados();
                Incidencia actual = datos.Listar().Find(x => x.IdIncidencia == int.Parse(id));
                Cliente cliente = datosClientes.Listar().Find(x => x.IdCliente == actual.IdCliente);
                Empleado empleado = datosEmpleados.listar().Find(x => x.IDEmpleado == actual.IdEmpleado);

                txtDNI.Text = cliente.DNI.ToString();
                txtMail.Text = cliente.Usuario.Email;
                txtTelefono.Text = cliente.Telefono.ToString();
                ddlTipo.SelectedValue = actual.tipo.IDTipo.ToString();
                ddlPrioridad.SelectedValue = actual.prioridad.IDPrioridad.ToString();
                if (cliente.Categoria.IDCategoria != 0) ddlCategoria.SelectedValue = cliente.Categoria.IDCategoria.ToString();
                else
                {
                    ddlCategoria.Items.Insert(0, new ListItem("Sin Asignacion"));
                    ddlCategoria.SelectedIndex = 0;
                }
                lblFechaYHora.Text = actual.FechaYHoraCreacion.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEstadoActual.Text = actual.EstadoActual.ToString();
                txtDescripcion.Text = actual.Descripcion;
                txtResolucion.Text = actual.Resolucion;
                txtApellido.Enabled = false;
                txtDescripcion.Enabled = false;
                ddlCategoria.Enabled = false;

                if (actual.FechaYHoraResolucion != DateTime.MaxValue)
                {
                    btnCargar.Text = "Reabrir";

                    txtResolucion.Enabled = false;
                    ddlTipo.Enabled = false;
                    txtEstadoActual.Enabled = false;
                    ddlPrioridad.Enabled = false;
                }
            }
        }

        protected void BtnCargar_Click(object sender, EventArgs e)
        {
            AccesoIncidencias entry = new AccesoIncidencias();
            Usuario user = (Usuario)Session["usuario"];
            EmailService emailService = new EmailService();
            AccesoClientes dataCli = new AccesoClientes();
            AccesoEmpleados dataEmp = new AccesoEmpleados();
            AccesoHistorial datahist = new AccesoHistorial();

            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                Incidencia nueva = new Incidencia
                {
                    IdCliente = (dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).IdCliente,

                    tipo = new TiposIncidente
                    {
                        IDTipo = int.Parse(ddlTipo.SelectedValue)
                    },

                    prioridad = new PrioridadesIncidente
                    {
                        IDPrioridad = int.Parse(ddlPrioridad.SelectedValue)
                    },
                    Descripcion = txtDescripcion.Text,
                    FechaYHoraCreacion = DateTime.Parse(lblFechaYHora.Text)
                };
                int IDIncidencia = entry.AgregarIncidencia(nueva);
                nueva.IdIncidencia = IDIncidencia;
                nueva.EstadoActual = "Pendiente";
                datahist.Agregar(nueva);
                emailService.ArmarCorreo(txtMail.Text, "Se genero la Incidencia Numero " + IDIncidencia, "Estos son los datos: <br>" + txtDescripcion.Text);
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    Incidencia nueva = entry.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                    if (nueva.FechaYHoraResolucion != DateTime.MaxValue)
                    {
                        entry.ReactivarIncidencia(nueva);
                        emailService.ArmarCorreo(txtMail.Text, "Se realizo la reactivacion de la Incidencia " + nueva.IdIncidencia + ".", "Estos son los datos: <br>" + txtDescripcion.Text);
                    }
                    else
                    {
                        nueva.tipo.IDTipo = int.Parse(ddlTipo.SelectedValue);
                        nueva.prioridad.IDPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                        nueva.EstadoActual = txtEstadoActual.Text;
                        if (!(string.IsNullOrEmpty(txtResolucion.Text)))
                        {
                            nueva.EstadoActual = "Cerrado";
                            nueva.FechaYHoraResolucion = DateTime.Now;
                            nueva.Resolucion = txtResolucion.Text;
                            emailService.ArmarCorreo(txtMail.Text, "Se realiza el cierre de la Incidencia " + nueva.IdIncidencia, "Se detallan los motivos: <br>" + txtResolucion.Text);
                        }
                        else
                        {
                            nueva.Resolucion = null;
                        }
                        entry.ModificarIncidencia(nueva);

                    }
                }
                else
                {
                    if (ValidacionesCliente())
                    {
                        AccesoUsuario dataUser = new AccesoUsuario();
                        Cliente nuevo = new Cliente
                        {
                            Usuario = new Usuario
                            {
                                TipoUsuario = TipoUsuario.Cliente,
                                Email = txtMail.Text,
                                Clave = "password"
                            },
                            DNI = txtDNI.Text,
                            Nombre = txtNombre.Text,
                            Apellido = txtApellido.Text,
                            Telefono = txtTelefono.Text,
                            Categoria = new CategoriasCliente
                            {
                                IDCategoria = int.Parse(ddlCategoria.SelectedValue),
                            }
                        };
                        foreach (Usuario usuario in dataUser.Listar())
                        {
                            if (usuario.Email == txtMail.Text && usuario.TipoUsuario != TipoUsuario.Cliente)
                            {
                                lblRegistro.Text = "El mail ya se encuentra registrado...";
                                lblRegistro.Visible = true;
                                return;
                            }
                        }
                        foreach (Empleado emp in dataEmp.listar())
                        {
                            if (emp.DNI == txtDNI.Text)
                            {
                                lblRegistro.Text = "El DNI ya se encuentra registrado...";
                                lblRegistro.Visible = true;
                                return;
                            }
                        }

                        dataCli.AgregarCliente(nuevo);
                    }

                    Incidencia inc = new Incidencia
                    {
                        IdCliente = (dataCli.Listar().Find(x => x.DNI == txtDNI.Text)).IdCliente,
                        IdEmpleado = (dataEmp.listar().Find(x => x.IDUsuario == user.IdUsuario)).IDEmpleado,

                        tipo = new TiposIncidente
                        {
                            IDTipo = int.Parse(ddlTipo.SelectedValue)
                        },

                        prioridad = new PrioridadesIncidente
                        {
                            IDPrioridad = int.Parse(ddlPrioridad.SelectedValue)
                        },
                        Descripcion = txtDescripcion.Text,
                        FechaYHoraCreacion = DateTime.Parse(lblFechaYHora.Text)
                    };
                    int IDIncidencia = entry.AgregarIncidencia(inc);
                    emailService.ArmarCorreo(txtMail.Text, "Se genero la Incidencia Numero " + IDIncidencia, "Estos son los datos: <br>" + txtDescripcion.Text);
                }
            }

            try
            {
                emailService.EnviarEmail();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
            Response.Redirect("Formularios.aspx");
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
        protected void TxtResolucion_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResolucion.Text))
            {
                btnCargar.Text = "Cerrar";
                btnCargar.CssClass = "btn btn-danger btn-lg mx-3";
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formularios.aspx");
        }
        public void CargarDatosCliente()
        {
            AccesoClientes datos = new AccesoClientes();
            Usuario user = ((Usuario)Session["usuario"]);
            Cliente cli = datos.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
            txtApellido.Enabled = false;
            txtResolucion.Visible = false;
            lblResolucion.Visible = false;
            lblEstadoActual.Visible = false;
            lblPrioridad.Visible = false;
            txtEstadoActual.Visible = false;
            ddlPrioridad.Visible = false;
            ddlCategoria.Enabled = false;

            lblFechaYHora.Text = DateTime.Now.ToString();
            txtNombre.Text = cli.Nombre;
            txtApellido.Text = cli.Apellido;
            txtMail.Text = cli.Usuario.Email;
            txtDNI.Text = cli.DNI.ToString();
            txtTelefono.Text = cli.Telefono.ToString();
            if (cli.Categoria.IDCategoria != 0) ddlCategoria.SelectedValue = cli.Categoria.IDCategoria.ToString();
            else
            {
                ddlCategoria.Items.Insert(0, new ListItem("Sin Asignacion"));
                ddlCategoria.SelectedIndex = 0;
            }
        }
        public void CargarCategoria()
        {
            if (!IsPostBack)
            {
                AccesoCategorias datos = new AccesoCategorias();
                try
                {
                    ddlCategoria.DataSource = datos.Listar();
                    ddlCategoria.DataValueField = "IDCategoria";
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataBind();
                }
                catch (Exception er)
                {

                    throw er;
                }
            }
        }
        public bool ValidacionesCliente()
        {
            AccesoClientes dataCli = new AccesoClientes();
            AccesoEmpleados dataEmp = new AccesoEmpleados();
            foreach (Cliente aux in dataCli.Listar())
            {
                if (txtDNI.Text == aux.DNI)
                {
                    txtDNI.Text = aux.DNI;
                    txtNombre.Text = aux.Nombre;
                    txtApellido.Text = aux.Apellido;
                    txtMail.Text = aux.Usuario.Email;
                    txtTelefono.Text = aux.Telefono;
                    txtDNI.Enabled = false;
                    txtNombre.Enabled = false;
                    txtTelefono.Enabled = false;
                    ddlCategoria.Enabled = false;
                    return false;
                }
                else if (txtTelefono.Text == aux.Telefono)
                {
                    txtDNI.Text = aux.DNI;
                    txtNombre.Text = aux.Nombre;
                    txtApellido.Text = aux.Apellido;
                    txtMail.Text = aux.Usuario.Email;
                    txtTelefono.Text = aux.Telefono;
                    txtDNI.Enabled = false;
                    txtNombre.Enabled = false;
                    txtTelefono.Enabled = false;
                    ddlCategoria.Enabled = false;
                    return false;
                }
            }
            return true;
        }

        protected void txtMail_TextChanged(object sender, EventArgs e)
        {
            AccesoClientes dataCli = new AccesoClientes();
            foreach (Cliente aux in dataCli.Listar())
            {
                if (txtMail.Text == aux.Usuario.Email)
                {
                    txtDNI.Text = aux.DNI;
                    txtNombre.Text = aux.Nombre;
                    txtApellido.Text = aux.Apellido;
                    txtMail.Text = aux.Usuario.Email;
                    txtTelefono.Text = aux.Telefono;
                    txtApellido.Enabled = false;
                    txtDNI.Enabled = false;
                    txtNombre.Enabled = false;
                    txtTelefono.Enabled = false;
                    ddlCategoria.Enabled = false;
                    return;
                }
            }
        }

        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            AccesoClientes dataCli = new AccesoClientes();
            foreach (Cliente aux in dataCli.Listar())
            {
                if (txtDNI.Text == aux.DNI)
                {
                    txtDNI.Text = aux.DNI;
                    txtNombre.Text = aux.Nombre;
                    txtApellido.Text = aux.Apellido;
                    txtMail.Text = aux.Usuario.Email;
                    txtTelefono.Text = aux.Telefono;
                    txtApellido.Enabled = false;
                    txtMail.Enabled = false;
                    txtNombre.Enabled = false;
                    txtTelefono.Enabled = false;
                    ddlCategoria.Enabled = false;
                    return;
                }
            }
        }

        protected void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            AccesoClientes dataCli = new AccesoClientes();
            foreach (Cliente aux in dataCli.Listar())
            {
                if (txtTelefono.Text == aux.Telefono)
                {
                    txtDNI.Text = aux.DNI;
                    txtNombre.Text = aux.Nombre;
                    txtApellido.Text = aux.Apellido;
                    txtMail.Text = aux.Usuario.Email;
                    txtTelefono.Text = aux.Telefono;
                    txtApellido.Enabled = false;
                    txtMail.Enabled = false;
                    txtNombre.Enabled = false;
                    txtDNI.Enabled = false;
                    ddlCategoria.Enabled = false;
                    return;
                }
            }
        }
    }
}