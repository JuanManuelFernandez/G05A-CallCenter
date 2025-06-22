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
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
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
            };

            lblRegistro.Visible = true;

            try
            {
                Usuario user = accesoUsuario.Listar().Find(x => x.Email == email.Text) != null ? accesoUsuario.Listar().Find(x => x.Email == email.Text) : null;
                if(user == null)
                {
                    accesoClientes.AgregarCliente(nuevoCliente);
                }
                else if(user.Eliminado == true)
                {
                    nuevoCliente.Usuario.IdUsuario = user.IdUsuario;
                    accesoUsuario.ActivarUsuarioConEmail(email.Text);
                    accesoClientes.ModificarCliente(nuevoCliente);
                }
                else if(user.Eliminado == false)
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
                Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}