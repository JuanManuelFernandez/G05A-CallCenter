using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace GP05_CallCenter
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            AccesoClientes accesoClientes = new AccesoClientes();
            Clientes nuevoCliente = new Clientes
            {
                Dni = dni.Text,
                Nombre = nombre.Text,
                Apellido = apellido.Text,
                Email = mail.Text,
                Telefono = telefono.Text,
                Contraseña = contraseña.Text,
            };

            lblRegistro.Visible = true;

            try
            {
                dni.Text = "";
                nombre.Text = "";
                apellido.Text = "";
                mail.Text = "";
                telefono.Text = "";
                contraseña.Text = "";
                accesoClientes.AgregarCliente(nuevoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}