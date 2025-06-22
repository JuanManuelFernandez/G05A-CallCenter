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
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario != TipoUsuario.Admin) {
                    Response.Redirect("Inicio.aspx");
                }
            }
            else
            {
                Response.Redirect("Inicio.aspx");
            }
            if (!IsPostBack)
            {
                if ((Request.QueryString["IdUsuario"]) != null)
                {
                    AccesoUsuario dataUser = new AccesoUsuario();
                    AccesoEmpleados dataEmp = new AccesoEmpleados();
                    AccesoClientes dataClientes = new AccesoClientes();
                    Usuario user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));


                    if (user.Eliminado)
                    {
                        btnEliminar.Text = "Activar";
                        btnEliminar.CssClass = "btn btn-success btn-lg";
                    }
                    if (user.TipoUsuario.ToString() == "Cliente")
                    {
                        cargarCategoria();
                        Cliente cliente = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                        txtDNI.Text = cliente.DNI.ToString();
                        txtNombre.Text = cliente.Nombre.ToString();
                        txtApellido.Text = cliente.Apellido.ToString();
                        txtEmail.Text = cliente.Usuario.Email.ToString();
                        txtTelefono.Text = cliente.Telefono.ToString();
                        ddlCategoria.SelectedValue = cliente.Categoria.IDCategoria.ToString();
                    }
                    else if (user.TipoUsuario.ToString() == "Empleado")
                    {
                        Empleado emp = dataEmp.BuscarPorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                        lblCategoria.Visible = false;
                        ddlCategoria.Visible = false;
                        lblTelefono.Text = "Legajo";
                        txtTelefono.TextMode = TextBoxMode.SingleLine;
                        txtDNI.Text = emp.DNI.ToString();
                        txtNombre.Text = emp.Nombre;
                        txtApellido.Text = emp.Apellido;
                        txtEmail.Text = user.Email;
                        txtTelefono.Text = emp.Legajo.ToString();

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
                nuevo.Categoria = new CategoriasCliente();
                nuevo.Categoria.IDCategoria = int.Parse(ddlCategoria.SelectedValue);
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
                data.EliminarUsuarioID(int.Parse(Request.QueryString["IdUsuario"]));
                Response.Redirect("Admin.aspx");
            }
        }
        public void cargarCategoria()
        {
            AccesoCategorias datos = new AccesoCategorias();
            try
            {
                ddlCategoria.DataSource = datos.Listar();
                ddlCategoria.DataValueField = "IDCategoria";
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataBind();
            }
            catch (Exception er)
            {

                throw er;
            }
        }
    }
}