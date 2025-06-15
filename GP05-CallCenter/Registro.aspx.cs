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
            {
                nuevoCliente.DNI = dni.Text;
                nuevoCliente.Nombre = nombre.Text;
                nuevoCliente.Apellido = apellido.Text;
                nuevoCliente.Telefono = telefono.Text;
            }
            nuevoCliente.Usuario = new Usuario();
            {
                nuevoCliente.Email = email.Text;
                nuevoCliente.Usuario.Clave = clave.Text;
            };

            lblRegistro.Visible = true;

            try
            {
                dni.Text = "";
                nombre.Text = "";
                apellido.Text = "";
                email.Text = "";
                telefono.Text = "";
                clave.Text = "";
                accesoClientes.AgregarCliente(nuevoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}