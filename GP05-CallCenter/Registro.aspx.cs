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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
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
            Cliente nuevoCliente = new Cliente();
            AccesoUsuario accesoUsuario = new AccesoUsuario();
            AccesoClientes accesoCliente = new AccesoClientes();
            {
                nuevoCliente.DNI = int.Parse(dni.Text);
                nuevoCliente.Nombre = nombre.Text;
                nuevoCliente.Apellido = apellido.Text;
                nuevoCliente.Telefono = telefono.Text;
            }
            nuevoCliente.Usuario = new Usuario();
            {
                nuevoCliente.Usuario.Email = email.Text;
                nuevoCliente.Usuario.Clave = clave.Text;
            }
            ;

            try
            {
                if (accesoCliente.Listar().Find(x => x.Usuario.Email == nuevoCliente.Usuario.Email && x.DNI == nuevoCliente.DNI && x.Usuario.Clave == nuevoCliente.Usuario.Clave && x.Usuario.Eliminado == true) != null)
                {
                    nuevoCliente.Usuario.IdUsuario = accesoCliente.BuscarClientePorDNI(nuevoCliente.DNI).Usuario.IdUsuario;
                    accesoUsuario.ActivarUsuarioConEmail(email.Text);
                    accesoClientes.ModificarCliente(nuevoCliente);
                }
                else if (accesoCliente.Listar().Find(x => x.DNI == nuevoCliente.DNI && x.Usuario.Eliminado == false) != null)
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un cliente con ese DNI.";
                    lblRegistro.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (accesoCliente.Listar().Find(x => x.Usuario.Email == nuevoCliente.Usuario.Email) != null)
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un cliente con ese Email.";
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
            AccesoUsuario dataUser = new AccesoUsuario();
            Empleado nuevo = new Empleado();
            Usuario user = new Usuario();
            {
                nuevo.DNI = int.Parse(dni.Text);
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