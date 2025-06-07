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
                dni = TextBox1.Text,
                nombre = TextBox2.Text,
                apellido = TextBox3.Text,
                email = TextBox4.Text,
                telefono = TextBox5.Text,
                contraseña = TextBox6.Text,
            };

            LblRegistro.Visible = true;

            try
            {
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                accesoClientes.AgregarCliente(nuevoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}