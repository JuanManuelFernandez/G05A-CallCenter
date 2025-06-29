using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
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
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    TituloH1 = "¡Bienvenido Administrador/a!";
                    ParrafoP = "Aqui podra agregar Empleados.";
                    telefono.Attributes["Placeholder"] = "Legajo";
                    telefono.Attributes["Textmode"] = "";
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (Session["usuario"] != null)
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
            AccesoClientes accesoClientes = new AccesoClientes();
            AccesoEmpleados accesoEmpleados = new AccesoEmpleados();
            Cliente nuevoCliente = new Cliente();
            AccesoUsuario accesoUsuario = new AccesoUsuario();
            AccesoClientes accesoCliente = new AccesoClientes();
            {
                nuevoCliente.DNI = dni.Text;
                nuevoCliente.Nombre = nombre.Text;
                nuevoCliente.Apellido = apellido.Text;
                nuevoCliente.Telefono = telefono.Text;
            }
            nuevoCliente.Usuario = new Usuario();
            {
                nuevoCliente.Usuario.Email = email.Text;
                nuevoCliente.Usuario.Clave = clave.Text;
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
                    nuevoCliente.Usuario.IdUsuario = accesoCliente.BuscarClientePorDNI(nuevoCliente.DNI).Usuario.IdUsuario;
                    accesoUsuario.ActivarUsuarioConEmail(email.Text);
                    accesoClientes.ModificarCliente(nuevoCliente);
                }
                else if (accesoCliente.VerificarDNI(nuevoCliente.DNI) || accesoEmpleados.VerificarDNI(nuevoCliente.DNI))
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
                Session.Add("usuario", (Usuario)accesoUsuario.Listar().Find(x => x.Email == nuevoCliente.Usuario.Email));
                Response.Redirect("Inicio.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarEmpleado()
        {
            AccesoEmpleados dataEmp = new AccesoEmpleados();
            AccesoClientes dataCli = new AccesoClientes();
            AccesoUsuario dataUser = new AccesoUsuario();
            Empleado nuevo = new Empleado();
            Usuario user = new Usuario();
            {
                nuevo.DNI = dni.Text;
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
                if (dataEmp.VerificarDNI(nuevo.DNI) || dataCli.VerificarDNI(nuevo.DNI))
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
                nuevo.IDUsuario = (dataUser.Listar()[(dataUser.Listar().Count) - 1]).IdUsuario;
                dataEmp.AgregarEmpleado(nuevo);
            }
            catch (Exception er)
            {

                throw er;
            }
            Response.Redirect("Inicio.aspx");
        }
    }
}