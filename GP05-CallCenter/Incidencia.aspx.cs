using System;
using System.Linq;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace CallCenter
{
    public partial class Incidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTipo();
                CargarPrioridad();
                CargarCategoria();
                CargarPlantilla();
            }

            var user = (Usuario)Session["usuario"];
            if (Session["usuario"] == null)
            {
                Response.Redirect("inicio.aspx");
            }
            // Pagina para...
            // Cliente
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                lblPlantilla.Visible = false;
                ddlPlantillas.Visible = false;
                btnAplicarPlantilla.Visible = false;

                btnActualizar.Visible = false;
                btnActualizarCliente.Visible = false;

            }
            // Cuando recibo URL de caso ya existente
            if (Request.QueryString["id"] != null)
            {
                btnCargar.Text = "Modificar";
                btnCargar.CssClass = "btn btn-success btn-lg mx-3";
                CargarIncidencia();
                // Deshabilito Plantillas
                if (txtDescripcion.Enabled == false)
                {
                    lblPlantilla.Visible = false;
                    ddlPlantillas.Visible = false;
                    btnAplicarPlantilla.Visible = false;
                }
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
                var id = Request.QueryString["id"].ToString();

                var datosInc = new AccesoIncidencias();
                var datosCli = new AccesoClientes();
                var datosEmp = new AccesoEmpleados();
                var incActual = datosInc.Listar().Find(x => x.IdIncidencia == int.Parse(id));
                var cliente = datosCli.Listar().Find(x => x.IdCliente == incActual.IdCliente);
                var empleado = datosEmp.Listar().Find(x => x.IdEmpleado == incActual.IdEmpleado);

                txtDNI.Text = cliente.Dni.ToString();
                txtMail.Text = cliente.Usuario.Email;
                txtTelefono.Text = cliente.Telefono.ToString();
                ddlTipo.SelectedValue = incActual.Tipo.IdTipo.ToString();
                ddlPrioridad.SelectedValue = incActual.Prioridad.IdPrioridad.ToString();
                if (cliente.Categoria.IdCategoria != 0) ddlCategoria.SelectedValue = cliente.Categoria.IdCategoria.ToString();
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
                txtDescripcion.Enabled = true;
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
            var dataInc = new AccesoIncidencias();
            var user = (Usuario)Session["usuario"];
            var emailService = new EmailService();
            var dataCli = new AccesoClientes();
            var dataEmp = new AccesoEmpleados();

            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                var nuevaInc = new Incidencias
                {
                    EstadoActual = "Pendiente",
                    IdCliente = (dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).IdCliente,

                    Tipo = new TiposIncidente
                    {
                        IdTipo = int.Parse(ddlTipo.SelectedValue)
                    },

                    Prioridad = new PrioridadesIncidente
                    {
                        IdPrioridad = int.Parse(ddlPrioridad.SelectedValue)
                    },
                    Descripcion = txtDescripcion.Text,
                    FechaYHoraCreacion = DateTime.Parse(lblFechaYHora.Text)
                };
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    lblError.Text = "La descripcion no puede estar vacia.";
                    lblError.Visible = true;
                    return;
                }
                var idIncidencia = dataInc.AgregarIncidencia(nuevaInc);
                nuevaInc.IdIncidencia = idIncidencia;
                nuevaInc.EstadoActual = "Pendiente";
                emailService.ArmarCorreo(txtMail.Text, "Creacion de Incidencias #" + idIncidencia, "Descripcion: <br>" + txtDescripcion.Text);

            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    var nuevaInc = dataInc.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                    if (nuevaInc.FechaYHoraResolucion != DateTime.MaxValue)
                    {
                        dataInc.ReactivarIncidencia(nuevaInc);
                        emailService.ArmarCorreo(txtMail.Text, "Reactivacion de Incidencias #" + nuevaInc.IdIncidencia + ".", "Descripcion: <br>" + txtDescripcion.Text);
                    }
                    else
                    {
                        nuevaInc.Tipo.IdTipo = int.Parse(ddlTipo.SelectedValue);
                        nuevaInc.Prioridad.IdPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                        nuevaInc.EstadoActual = txtEstadoActual.Text;
                        nuevaInc.Descripcion = txtDescripcion.Text;

                        // Si Resolucion esta vacio, cierra la Incidencias
                        if (!(string.IsNullOrEmpty(txtResolucion.Text)))
                        {
                            nuevaInc.EstadoActual = "Cerrado";
                            nuevaInc.FechaYHoraResolucion = DateTime.Now;
                            nuevaInc.Resolucion = txtResolucion.Text;
                            emailService.ArmarCorreo(txtMail.Text, "Cierre de Incidencias #" + nuevaInc.IdIncidencia, "Motivos: <br>" + txtResolucion.Text);
                        }
                        else
                        {
                            nuevaInc.Resolucion = null;
                        }
                        if (string.IsNullOrEmpty(txtEstadoActual.Text))
                        {
                            lblError.Text = "El estado no puede estar vacio.";
                            lblError.Visible = true;
                            return;
                        }
                        dataInc.ModificarIncidencia(nuevaInc);
                    }
                }
                else
                {
                    if (ValidacionesCliente())
                    {
                        var dataUser = new AccesoUsuario();
                        var nuevoCli = new Cliente
                        {
                            Usuario = new Usuario
                            {
                                TipoUsuario = TipoUsuario.Cliente,
                                Email = txtMail.Text,
                                Clave = "password"
                            },
                            Dni = txtDNI.Text,
                            Nombre = txtNombre.Text,
                            Apellido = txtApellido.Text,
                            Telefono = txtTelefono.Text,
                            Categoria = new CategoriasCliente
                            {
                                IdCategoria = int.Parse(ddlCategoria.SelectedValue),
                            }
                        };
                        if (dataUser.Listar().Any(usuario => usuario.Email == txtMail.Text && usuario.TipoUsuario != TipoUsuario.Cliente))
                        {
                            lblError.Text = "El mail ya se encuentra registrado...";
                            lblError.Visible = true;
                            return;
                        }
                        if (dataEmp.Listar().Any(emp => emp.Dni == txtDNI.Text))
                        {
                            lblError.Text = "El DNI ya se encuentra registrado...";
                            lblError.Visible = true;
                            return;
                        }

                        dataCli.AgregarCliente(nuevoCli);
                    }

                    var inc = new Incidencias
                    {
                        IdCliente = (dataCli.Listar().Find(x => x.Dni == txtDNI.Text)).IdCliente,
                        IdEmpleado = (dataEmp.Listar().Find(x => x.IdUsuario == user.IdUsuario)).IdEmpleado,
                        EstadoActual = txtEstadoActual.Text,
                        Tipo = new TiposIncidente
                        {
                            IdTipo = int.Parse(ddlTipo.SelectedValue)
                        },

                        Prioridad = new PrioridadesIncidente
                        {
                            IdPrioridad = int.Parse(ddlPrioridad.SelectedValue)
                        },
                        Descripcion = txtDescripcion.Text,
                        FechaYHoraCreacion = DateTime.Parse(lblFechaYHora.Text)
                    };
                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        lblError.Text = "La descripcion no puede estar vacia.";
                        lblError.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtEstadoActual.Text))
                    {
                        lblError.Text = "El estado no puede estar vacio.";
                        lblError.Visible = true;
                        return;
                    }
                    var idIncidencia = dataInc.AgregarIncidencia(inc);
                    emailService.ArmarCorreo(txtMail.Text, "Creacion de Incidencias #" + idIncidencia, "Descripcion: <br>" + txtDescripcion.Text);
                }
            }

            try
            {
                emailService.EnviarEmail();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("error.aspx");
            }
            Response.Redirect("reclamos.aspx");
        }
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            var dataInc = new AccesoIncidencias();
            var user = (Usuario)Session["usuario"];
            var dataCli = new AccesoClientes(); ;

            //BORRAR?

            //if (user.TipoUsuario == TipoUsuario.Cliente)
            //{
            //    Incidencias nuevaInc = new Incidencias
            //    {
            //        EstadoActual = "Pendiente",
            //        IdCliente = (dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario)).IdCliente,
            //    };
            //    if (string.IsNullOrEmpty(txtDescripcion.Text))
            //    {
            //        lblRegistro.Text = "La descripcion no puede estar vacia.";
            //        lblRegistro.Visible = true;
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(txtEstadoActual.Text))
            //    {
            //        lblRegistro.Text = "El estado no puede estar vacio.";
            //        lblRegistro.Visible = true;
            //        return;
            //    }
            //    int IDIncidencia = dataInc.AgregarIncidencia(nuevaInc);
            //    nuevaInc.IdIncidencia = IDIncidencia;
            //    nuevaInc.EstadoActual = "Pendiente";
            //}
            //else
            //{
            //    if (Request.QueryString["id"] != null)
            //    {
            //        Incidencias nuevaInc = dataInc.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
            //        if (nuevaInc != null)
            //        {
            //            nuevaInc.Tipo.IDTipo = int.Parse(ddlTipo.SelectedValue);
            //            nuevaInc.Prioridad.IDPrioridad = int.Parse(ddlPrioridad.SelectedValue);
            //            nuevaInc.EstadoActual = txtEstadoActual.Text;
            //            nuevaInc.Descripcion = txtDescripcion.Text;
            //        }
            //        if (string.IsNullOrEmpty(txtEstadoActual.Text))
            //        {
            //            lblRegistro.Text = "El estado no puede estar vacio.";
            //            lblRegistro.Visible = true;
            //            return;
            //        }
            //        dataInc.ModificarIncidencia(nuevaInc);
            //        Response.Redirect("reclamos.aspx");
            //    }
            //}
            if (Request.QueryString["id"] != null)
            {
                var nuevaInc = dataInc.Listar().Find(x => x.IdIncidencia == int.Parse(Request.QueryString["id"]));
                if (nuevaInc != null)
                {
                    nuevaInc.Tipo.IdTipo = int.Parse(ddlTipo.SelectedValue);
                    nuevaInc.Prioridad.IdPrioridad = int.Parse(ddlPrioridad.SelectedValue);
                    nuevaInc.EstadoActual = txtEstadoActual.Text;
                    nuevaInc.Descripcion = txtDescripcion.Text;
                }
                if (string.IsNullOrEmpty(txtEstadoActual.Text))
                {
                    lblError.Text = "El estado no puede estar vacio.";
                    lblError.Visible = true;
                    return;
                }
                dataInc.ModificarIncidencia(nuevaInc);
                Response.Redirect("reclamos.aspx");
            }
        }
        protected void BtnActualizarCliente_Click(object sender, EventArgs e)
        {
            var dataCli = new AccesoClientes();
            var cli = dataCli.Listar().Find(x => x.Dni == txtDNI.Text);
            var id = cli.Usuario.IdUsuario;
            Response.Redirect("editarcuenta.aspx?IdUsuario=" + id);
        }
        public void CargarTipo()
        {
            var datos = new AccesoTipos();
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
        public void CargarPrioridad()
        {
            var datos = new AccesoPrioridades();
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
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("reclamos.aspx");
        }
        public void CargarDatosCliente()
        {
            var user = ((Usuario)Session["usuario"]);
            var dataCli = new AccesoClientes();
            var cli = dataCli.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
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
            txtDNI.Text = cli.Dni.ToString();
            txtTelefono.Text = cli.Telefono.ToString();
            if (cli.Categoria.IdCategoria != 0) ddlCategoria.SelectedValue = cli.Categoria.IdCategoria.ToString();
            else
            {
                ddlCategoria.Items.Insert(0, new ListItem("Sin Asignacion"));
                ddlCategoria.SelectedIndex = 0;
            }
        }
        public void CargarCategoria()
        {
            var datos = new AccesoCategorias();
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
        public void CargarPlantilla()
        {
            var datos = new AccesoPlantillas();
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
        protected void BtnAplicarPlantilla_Click(object sender, EventArgs e)
        {
            if (ddlPlantillas.SelectedValue != null)
            {
                var data = new AccesoPlantillas();
                int idPlantilla = int.Parse(ddlPlantillas.SelectedValue);
                var plantilla = data.Listar().Find(x => x.IdPlantilla == idPlantilla);
                if (plantilla != null)
                {
                    txtDescripcion.Text = plantilla.Descripcion;
                }
            }

            //if (ddlPlantillas.SelectedItem.Text == "Caso inactivo")
            //{
            //    var data = new AccesoPlantillas();
            //    txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            //}
            //else if (ddlPlantillas.SelectedItem.Text == "Completar datos")
            //{
            //    var data = new AccesoPlantillas();
            //    txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            //}
            //else if (ddlPlantillas.SelectedItem.Text == "Derivaciones")
            //{
            //    var data = new AccesoPlantillas();
            //    txtDescripcion.Text = (data.Listar().Find(x => x.IdPlantilla == int.Parse(ddlPlantillas.SelectedValue))).Descripcion;
            //}
        }

        public bool ValidacionesCliente()
        {
            var dataCli = new AccesoClientes();
            foreach (var aux in dataCli.Listar().Where(aux => txtDNI.Text == aux.Dni || txtTelefono.Text == aux.Telefono))
            {
                txtDNI.Text = aux.Dni;
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
            return true;
        }
        protected void TxtMail_TextChanged(object sender, EventArgs e)
        {
            var dataCli = new AccesoClientes();
            foreach (var aux in dataCli.Listar().Where(aux => txtMail.Text == aux.Usuario.Email))
            {
                txtDNI.Text = aux.Dni;
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
        protected void TxtDNI_TextChanged(object sender, EventArgs e)
        {
            var dataCli = new AccesoClientes();
            foreach (var aux in dataCli.Listar().Where(aux => txtDNI.Text == aux.Dni))
            {
                txtDNI.Text = aux.Dni;
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
        protected void TxtTelefono_TextChanged(object sender, EventArgs e)
        {
            var dataCli = new AccesoClientes();
            foreach (var aux in dataCli.Listar().Where(aux => txtTelefono.Text == aux.Telefono))
            {
                txtDNI.Text = aux.Dni;
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