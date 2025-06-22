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

            lblRegistro.Visible = true;

            try
            {
                Usuario user = accesoUsuario.Listar().Find(x => x.Email == email.Text) != null ? accesoUsuario.Listar().Find(x => x.Email == email.Text) : null;
                if (user == null)
                {
                    accesoClientes.AgregarCliente(nuevoCliente);
                }
                else if (user.Eliminado == true)
                {
                    nuevoCliente.Usuario.IdUsuario = user.IdUsuario;
                    accesoUsuario.ActivarUsuarioConEmail(email.Text);
                    accesoClientes.ModificarCliente(nuevoCliente);
                }
                else if (user.Eliminado == false)
                {
                    lblRegistro.Text = "Ya existe un usuario activo en el sistema con ese email.";
                    return;
                }
                dni.Text = "";
                nombre.Text = "";
                apellido.Text = "";
                email.Text = "";
                telefono.Text = "";
                clave.Text = "";
                Session.Add("usuario", (Usuario)accesoUsuario.Listar().Find(x => x.Email == nuevoCliente.Usuario.Email));
                Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarEmpleado() { 
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
                nuevo.IDUsuario = (dataUser.Listar()[(dataUser.Listar().Count)-1]).IdUsuario;
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