using System;
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
            CargarPlantilla();
            Usuario user = (Usuario)Session["usuario"];
            if (Session["usuario"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            // Pagina para...
            // Cliente
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                btnActualizar.Visible = false;
                btnActualizarCliente.Visible = false;

            }
            // Cuando recibo URL de caso ya existente
            if (Request.QueryString["id"] != null)
            {
                btnCargar.Text = "Modificar";
                btnCargar.CssClass = "btn btn-success btn-lg mx-3";
                CargarIncidencia();
                return;
            }
            // Empleado
            else if (user.TipoUsuario == TipoUsuario.Empleado)
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

                AccesoIncidencias datosInc = new AccesoIncidencias();
                AccesoClientes datosCli = new AccesoClientes();
                AccesoEmpleados datosEmp = new AccesoEmpleados();
                Incidencia incActual = datosInc.Listar().Find(x => x.IdIncidencia == int.Parse(id));
                Cliente cliente = datosCli.Listar().Find(x => x.IdCliente == incActual.IdCliente);
                Empleado empleado = datosEmp.listar().Find(x => x.IDEmpleado == incActual.IdEmpleado);

                txtDNI.Text = cliente.DNI.ToString();
                txtMail.Text = cliente.Usuario.Email;
                txtTelefono.Text = cliente.Telefono.ToString();
                ddlTipo.SelectedValue = incActual.tipo.IDTipo.ToString();
                ddlPrioridad.SelectedValue = incActual.prioridad.IDPrioridad.ToString();
                if (cliente.Categoria.IDCategoria != 0) ddlCategoria.SelectedValue = cliente.Categoria.IDCategoria.ToString();
                else
                {
                    ddlCategoria.Items.Insert(0, new ListItem("Sin Asignacion"));
                    ddlCategoria.SelectedIndex = 0;
                }
                lblFechaYHora.Text = incActual.FechaYHoraCreacion.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEstadoActual.Text = incActual.EstadoActual.ToString();
                txtDescripcion.Text = incActual.Descripcion;
                txtResolucion.Text = incActual.Resolucion;
                txtApellido.Enabled = false;
                txtDescripcion.Enabled = false;
                ddlCategoria.Enabled = false;

                if (incActual.FechaYHoraResolucion != DateTime.MaxValue)
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
            AccesoIncidencias dataInc = new AccesoIncidencias();
            Usuario user = (Usuario)Session["usuario"];
            EmailService emailService = new EmailService();
            AccesoClientes dataCli = new AccesoClientes();
            AccesoEmpleados dataEmp = new AccesoEmpleados();

            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                Incidencia nuevaInc = new Incidencia
                {
                    EstadoActual = "Pendiente",
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
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    lblRegistro.Text = "La descripcion no puede estar vacia.";
                    lblRegistro.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtEstadoActual.Text))
                {
                    lblRegistro.Text = "El estado no puede estar vacio.";
                    lblRegistro.Visible = true;
                    return;
                }
                int IDIncidencia = dataInc.AgregarIncidencia(nuevaInc);
                nuevaInc.IdIncidencia = IDIncidencia;
                nuevaInc.EstadoActual = "Pendiente";
                emailService.ArmarCorreo(txtMail.Text, "Creacion de Incidencia #" + IDIncidencia, "Descripcion: <br>" + txtDescripcion.Text);

            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    Incidencia nuevaInc = dataInc.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                    if (nuevaInc.FechaYHoraResolucion != DateTime.MaxValue)
                    {
                        dataInc.ReactivarIncidencia(nuevaInc);
                        emailService.ArmarCorreo(txtMail.Text, "Reactivacion de Incidencia #" + nuevaInc.IdIncidencia + ".", "Descripcion: <br>" + txtDescripcion.Text);
                    }
                    else
                    {
                        nuevaInc.tipo.IDTipo = int.Parse(ddlTipo.SelectedValue);
                        nuevaInc.prioridad.IDPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                        nuevaInc.EstadoActual = txtEstadoActual.Text;
                        nuevaInc.Descripcion = txtDescripcion.Text;

                        // Si Resolucion esta vacio, cierra la Incidencia
                        if (!(string.IsNullOrEmpty(txtResolucion.Text)))
                        {
                            nuevaInc.EstadoActual = "Cerrado";
                            nuevaInc.FechaYHoraResolucion = DateTime.Now;
                            nuevaInc.Resolucion = txtResolucion.Text;
                            emailService.ArmarCorreo(txtMail.Text, "Cierre de Incidencia #" + nuevaInc.IdIncidencia, "Motivos: <br>" + txtResolucion.Text);
                        }
                        else
                        {
                            nuevaInc.Resolucion = null;
                        }
                        if (string.IsNullOrEmpty(txtEstadoActual.Text))
                        {
                            lblRegistro.Text = "El estado no puede estar vacio.";
                            lblRegistro.Visible = true;
                            return;
                        }
                        dataInc.ModificarIncidencia(nuevaInc);
                    }
                }
                else
                {
                    if (ValidacionesCliente())
                    {
                        AccesoUsuario dataUser = new AccesoUsuario();
                        Cliente nuevoCli = new Cliente
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

                        dataCli.AgregarCliente(nuevoCli);
                    }

                    Incidencia inc = new Incidencia
                    {
                        IdCliente = (dataCli.Listar().Find(x => x.DNI == txtDNI.Text)).IdCliente,
                        IdEmpleado = (dataEmp.listar().Find(x => x.IDUsuario == user.IdUsuario)).IDEmpleado,
                        EstadoActual = txtEstadoActual.Text,
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
                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        lblRegistro.Text = "La descripcion no puede estar vacia.";
                        lblRegistro.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtEstadoActual.Text))
                    {
                        lblRegistro.Text = "El estado no puede estar vacio.";
                        lblRegistro.Visible = true;
                        return;
                    }
                    int IDIncidencia = dataInc.AgregarIncidencia(inc);
                    emailService.ArmarCorreo(txtMail.Text, "Creacion de Incidencia #" + IDIncidencia, "Descripcion: <br>" + txtDescripcion.Text);
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
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            AccesoIncidencias dataInc = new AccesoIncidencias();
            Usuario user = (Usuario)Session["usuario"];
            AccesoClientes dataCli = new AccesoClientes(); ;

            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                Incidencia nuevaInc = new Incidencia
                {
                    EstadoActual = "Pendiente",
                    IdCliente = (dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).IdCliente,
                };
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    lblRegistro.Text = "La descripcion no puede estar vacia.";
                    lblRegistro.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtEstadoActual.Text))
                {
                    lblRegistro.Text = "El estado no puede estar vacio.";
                    lblRegistro.Visible = true;
                    return;
                }
                int IDIncidencia = dataInc.AgregarIncidencia(nuevaInc);
                nuevaInc.IdIncidencia = IDIncidencia;
                nuevaInc.EstadoActual = "Pendiente";
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    Incidencia nuevaInc = dataInc.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                    if (nuevaInc != null)
                    {
                        nuevaInc.tipo.IDTipo = int.Parse(ddlTipo.SelectedValue);
                        nuevaInc.prioridad.IDPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                        nuevaInc.EstadoActual = txtEstadoActual.Text;
                        nuevaInc.Descripcion = txtDescripcion.Text;
                    }
                    if (string.IsNullOrEmpty(txtEstadoActual.Text))
                    {
                        lblRegistro.Text = "El estado no puede estar vacio.";
                        lblRegistro.Visible = true;
                        return;
                    }
                    dataInc.ModificarIncidencia(nuevaInc);
                    Response.Redirect("Formularios.aspx");
                }
            }
        }
        protected void BtnActualizarCliente_Click(object sender, EventArgs e)
        {
            AccesoClientes dataCli = new AccesoClientes();
            Cliente cli = dataCli.Listar().Find(x => x.DNI == txtDNI.Text);
            var id = cli.Usuario.IdUsuario;
            Response.Redirect("Edicion.aspx?IdUsuario=" + id);
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
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formularios.aspx");
        }
        public void CargarDatosCliente()
        {
            Usuario user = ((Usuario)Session["usuario"]);
            AccesoClientes dataCli = new AccesoClientes();
            Cliente cli = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
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
        public void CargarPlantilla()
        {
            if (!IsPostBack)
            {
                AccesoPlantillas datos = new AccesoPlantillas();
                try
                {
                    ddlPlantillas.DataSource = datos.Listar();
                    ddlPlantillas.DataValueField = "IDPlantilla";
                    ddlPlantillas.DataTextField = "Nombre";
                    ddlPlantillas.DataBind();
                }
                catch (Exception er)
                {
                    throw er;
                }
            }
        }
        protected void BtnAplicarPlantilla_Click(object sender, EventArgs e)
        {
            if (ddlPlantillas.SelectedItem.Text == "Caso inactivo")
            {
                AccesoPlantillas data = new AccesoPlantillas();
                txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            }
            else if (ddlPlantillas.SelectedItem.Text == "Completar datos")
            {
                AccesoPlantillas data = new AccesoPlantillas();
                txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            }
            else if (ddlPlantillas.SelectedItem.Text == "Derivaciones")
            {
                AccesoPlantillas data = new AccesoPlantillas();
                txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            }
        }

        public bool ValidacionesCliente()
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
        protected void TxtMail_TextChanged(object sender, EventArgs e)
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
        protected void TxtDNI_TextChanged(object sender, EventArgs e)
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
        protected void TxtTelefono_TextChanged(object sender, EventArgs e)
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