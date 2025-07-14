using System;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class Registro : System.Web.UI.Page
    {
        public string TituloH1 { get; set; }
        public string ParrafoP { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            TituloH1 = "¡Bienvenido/a!";
            ParrafoP = "¿Tu primera vez acá? Registrate y tomaremos tu pedido lo más pronto posible";
            if (Session["usuario"] != null)
            {
                var user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    telefono.TextMode = TextBoxMode.SingleLine;
                    TituloH1 = "¡Bienvenido Administrador/a!";
                    ParrafoP = "Aqui podra agregar Empleados.";
                    telefono.Attributes["Placeholder"] = "Legajo";
                    telefono.Attributes["Textmode"] = "";
                }
                else if(user.TipoUsuario == TipoUsuario.Empleado)
                {
                    TituloH1 = "¡Bienvenido Empleado/a!";
                    ParrafoP = "Aqui puedes registrar a un cliente como usuario en el caso de que no lo este";
                }
                else
                {
                    Response.Redirect("inicio.aspx");
                }
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["usuario"];
            if(user.TipoUsuario == TipoUsuario.Empleado)
            {
                CargarCliente();
            }
            else if (Session["usuario"] != null)
            {
                CargarEmpleado();
            }
            else
            {
                CargarCliente();
            }
        }
        public void CargarCliente()
        {
            var accesoClientes = new AccesoClientes();
            var accesoEmpleados = new AccesoEmpleados();
            var nuevoCliente = new Cliente();
            var accesoUsuario = new AccesoUsuario();
            var accesoCliente = new AccesoClientes();
            var user = (Usuario)Session["usuario"];
            {
                nuevoCliente.Dni = dni.Text;
                nuevoCliente.Nombre = nombre.Text;
                nuevoCliente.Apellido = apellido.Text;
                nuevoCliente.Telefono = telefono.Text;
            }
            nuevoCliente.Usuario = new Usuario();
            {
                nuevoCliente.Usuario.Email = email.Text;
                nuevoCliente.Usuario.Clave = clave.Text;
            }
            if(nuevoCliente.Usuario.Clave.Length < 8)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "La clave debe tener mínimo 8 caracteres...";
                lblRegistro.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (nuevoCliente.Telefono.Length < 8 || nuevoCliente.Telefono.Length > 15)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "El numero de telefono se debe tener entre 8 y 15 digitos...";
                lblRegistro.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                if (accesoCliente.VerificaReactivar(nuevoCliente))
                {
                    nuevoCliente.Usuario.IdUsuario = accesoCliente.BuscarClientePorDni(nuevoCliente.Dni).Usuario.IdUsuario;
                    accesoUsuario.ActivarUsuarioConEmail(email.Text);
                    accesoClientes.ModificarCliente(nuevoCliente);
                }
                else if (accesoCliente.VerificarDni(nuevoCliente.Dni) || accesoEmpleados.VerificarDni(nuevoCliente.Dni))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese DNI.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (accesoCliente.VerificarTelefono(nuevoCliente.Telefono))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un cliente con ese Telefono.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (accesoUsuario.VerificarEmail(nuevoCliente.Usuario.Email))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese Email.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    accesoClientes.AgregarCliente(nuevoCliente);
                }
                if(user.TipoUsuario != TipoUsuario.Empleado)
                {
                    Session.Add("usuario", (Usuario)accesoUsuario.Listar().Find(x => x.Email == nuevoCliente.Usuario.Email));
                    Response.Redirect("inicio.aspx", false);
                }
                if(user.TipoUsuario == TipoUsuario.Empleado)
                {
                    Response.Redirect("inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarEmpleado()
        {
            var dataEmp = new AccesoEmpleados();
            var dataCli = new AccesoClientes();
            var dataUser = new AccesoUsuario();
            var nuevo = new Empleado();
            var user = new Usuario();
            {
                nuevo.Dni = dni.Text;
                nuevo.Legajo = telefono.Text;
                nuevo.Nombre = nombre.Text;
                nuevo.Apellido = apellido.Text;
            }
            {
                user.Email = email.Text;
                user.Clave = clave.Text;
                user.TipoUsuario = TipoUsuario.Empleado;
            }
            try
            {
                if (dataEmp.VerificarDni(nuevo.Dni) || dataCli.VerificarDni(nuevo.Dni))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese DNI.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (dataUser.VerificarEmail(user.Email))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese Email.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (dataEmp.VerificarLegajo(nuevo.Legajo))
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un empleado con ese Legajo.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                dataUser.AgregarUsuario(user);
                nuevo.IdUsuario = (dataUser.Listar()[(dataUser.Listar().Count) - 1]).IdUsuario;
                dataEmp.AgregarEmpleado(nuevo);
            }
            catch (Exception er)
            {

                throw er;
            }
            Response.Redirect("inicio.aspx");
        }
    }
}