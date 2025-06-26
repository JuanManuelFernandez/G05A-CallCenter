using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class MisDatos : System.Web.UI.Page
    {
        private Usuario user;
        private Cliente cliente;
        private Empleado empleado;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblRegistro.Visible = false;
            if (Session["usuario"] == null)
            {
                Session.Add("error", "Debes loguearte para ver esta pagina");
                Response.Redirect("Error.aspx");
            }
            user = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {
                if (user.TipoUsuario == TipoUsuario.Cliente)
                {
                    AccesoClientes data = new AccesoClientes();
                    cliente = data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
                    txtEmail.Text = cliente.Usuario.Email;
                    txtDNI.Text = cliente.DNI.ToString();
                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtTelefono.Text = cliente.Telefono;
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    lblTelefono.Text = "Legajo";
                    txtTelefono.Enabled = false;
                    txtEmail.Enabled = false;
                    btnModificar.Visible = false;
                    AccesoEmpleados data = new AccesoEmpleados();
                    empleado = data.BuscarPorIdUsuario(user.IdUsuario);
                    txtEmail.Text = user.Email;
                    txtDNI.Text = empleado.DNI.ToString();
                    txtNombre.Text = empleado.Nombre;
                    txtApellido.Text = empleado.Apellido;
                    txtTelefono.Text = empleado.Legajo;
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (user.TipoUsuario == TipoUsuario.Cliente)
            {
                AccesoClientes data = new AccesoClientes();
                AccesoUsuario dataUser = new AccesoUsuario();
                AccesoEmpleados dataEmp = new AccesoEmpleados();

                string aux = txtTelefono.Text;

                if (aux.Length < 8 || aux.Length > 15)
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "El numero de telefono se debe tener entre 8 y 15 digitos...";
                    return;
                } else if (dataUser.Listar().Find(x => x.Email == txtEmail.Text && x.IdUsuario != ((Usuario)Session["usuario"]).IdUsuario) != null)
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese Email...";
                    return;
                }
                else if (data.Listar().Find(x => x.Telefono == txtTelefono.Text && x.Usuario.IdUsuario != ((Usuario)Session["usuario"]).IdUsuario) != null)
                {
                    lblRegistro.Visible = true;
                    lblRegistro.Text = "Ya existe un usuario con ese Telefono...";
                    return;
                }
                cliente = data.Listar().Find(x => x.Usuario.IdUsuario == user.IdUsuario);
                cliente.Usuario.Email = txtEmail.Text;
                cliente.Telefono = txtTelefono.Text;
                data.ModificarCliente(cliente);
                Session.Clear();
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}