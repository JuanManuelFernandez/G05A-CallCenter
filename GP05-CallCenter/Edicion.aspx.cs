using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class Edicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["IdUsuario"]) != null)
                {
                    AccesoUsuario dataUser = new AccesoUsuario();
                    AccesoClientes dataClientes = new AccesoClientes();
                    Usuario user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));
                    Cliente cliente = new Cliente();

                    if (user.Eliminado)
                    {
                        btnEliminar.Text = "Activar";
                        btnEliminar.CssClass = "btn btn-success btn-lg";
                    }
                    if (user.TipoUsuario.ToString() == "Cliente")
                    {
                        cliente = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                        txtDNI.Text = cliente.DNI.ToString();
                        txtNombre.Text = cliente.Nombre.ToString();
                        txtApellido.Text = cliente.Apellido.ToString();
                        txtEmail.Text = cliente.Usuario.Email.ToString();
                        txtTelefono.Text = cliente.Telefono.ToString();
                        txtCategoria.Text = cliente.IdCategoria.ToString();
                    }
                    else if (user.TipoUsuario.ToString() == "Empleado")
                    {

                    }
                    return;
                }
                else
                {

                }
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            AccesoUsuario dataUser = new AccesoUsuario();
            Usuario user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));
            if (user.TipoUsuario.ToString() == "Cliente")
            {
                AccesoClientes dataClientes = new AccesoClientes();
                Cliente cliente = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                Cliente nuevo = new Cliente();
                nuevo.Usuario = new Usuario();
                nuevo.IdCategoria = int.Parse(txtCategoria.Text);
                nuevo.DNI = int.Parse(txtDNI.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Usuario.IdUsuario = cliente.Usuario.IdUsuario;
                nuevo.Usuario.TipoUsuario = cliente.Usuario.TipoUsuario;
                nuevo.Usuario.Email = txtEmail.Text;
                nuevo.Usuario.Clave = cliente.Usuario.Clave;
                nuevo.Usuario.Eliminado = cliente.Usuario.Eliminado;
                dataClientes.ModificarCliente(nuevo);
                Response.Redirect("Admin.aspx");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            AccesoUsuario data = new AccesoUsuario();
            if (btnEliminar.Text == "Activar")
            {
                data.ActivarUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                Response.Redirect("Admin.aspx");
            }
            else
            {
                data.EliminarUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                Response.Redirect("Admin.aspx");
            }
        }
    }
}