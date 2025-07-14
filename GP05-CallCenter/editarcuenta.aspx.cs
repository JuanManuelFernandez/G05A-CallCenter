using Datos;
using System;
using System.Web.UI.WebControls;

namespace CallCenter
{
    public partial class EditarCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblRegistro.Visible = false;
            if (Session["usuario"] != null)
            {
                var user = (Usuario)Session["usuario"];
                // Cliente no puede editar sus datos
                if (user.TipoUsuario == TipoUsuario.Cliente)
                {
                    Response.Redirect("inicio.aspx");
                }
            }
            else
            {
                Response.Redirect("inicio.aspx");
            }
            if (!IsPostBack)
            {
                if ((Request.QueryString["IdUsuario"]) != null)
                {
                    var dataUser = new AccesoUsuario();
                    var dataEmp = new AccesoEmpleados();
                    var dataClientes = new AccesoClientes();
                    var user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));

                    if (user.Eliminado)
                    {
                        btnEliminar.Text = "Activar";
                        btnEliminar.CssClass = "btn btn-success btn-lg";
                    }
                    switch (user.TipoUsuario.ToString())
                    {
                        case "Cliente":
                        {
                            CargarCategoria();
                            var cliente = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                            txtDNI.Text = cliente.Dni.ToString();
                            txtNombre.Text = cliente.Nombre.ToString();
                            txtApellido.Text = cliente.Apellido.ToString();
                            txtEmail.Text = cliente.Usuario.Email.ToString();
                            txtTelefono.Text = cliente.Telefono.ToString();
                            ddlCategoria.SelectedValue = cliente.Categoria.IdCategoria.ToString();
                            break;
                        }
                        case "Empleado":
                        {
                            var emp = dataEmp.BuscarPorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));
                            lblCategoria.Visible = false;
                            ddlCategoria.Visible = false;
                            lblTelefono.Text = "Legajo";
                            txtTelefono.TextMode = TextBoxMode.SingleLine;
                            txtDNI.Text = emp.Dni.ToString();
                            txtNombre.Text = emp.Nombre;
                            txtApellido.Text = emp.Apellido;
                            txtEmail.Text = user.Email;
                            txtTelefono.Text = emp.Legajo.ToString();
                            break;
                        }
                    }
                    return;
                }
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            var dataUser = new AccesoUsuario();
            var user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));
            if (user.TipoUsuario.ToString() == "Cliente")
            {
                if (ModificarCliente()) {
                    Response.Redirect("admin.aspx");
                } 
            }
            else if (user.TipoUsuario == TipoUsuario.Empleado)
            {
                if (ModificarEmpleado())
                {
                    Response.Redirect("admin.aspx");
                }
            }
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var data = new AccesoUsuario();
            if (btnEliminar.Text == "Activar")
            {
                data.ActivarUsuario(int.Parse(Request.QueryString["IdUsuario"]));
            }
            else
            {
                data.EliminarUsuarioId(int.Parse(Request.QueryString["IdUsuario"]));
            }

            Response.Redirect("admin.aspx");
        }
        public bool ModificarEmpleado()
        {
            var dataClientes = new AccesoClientes();
            var dataUser = new AccesoUsuario();
            var user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));
            var dataEmpleados = new AccesoEmpleados();
            var nuevo = dataEmpleados.BuscarPorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));



            if (dataEmpleados.Listar().Find(x => x.Dni == txtDNI.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null || dataClientes.Listar().Find(x => x.Dni == txtDNI.Text && x.Usuario.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un usuario con ese DNI...";
                return false;
            }
            else if (dataUser.Listar().Find(x => x.Email == txtEmail.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un usuario con ese Email...";
                return false;
            }
            else if (dataEmpleados.Listar().Find(x => x.Legajo == txtTelefono.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un empleado con ese Legajo...";
                return false;
            }

            user.Email = txtEmail.Text;
            nuevo.Legajo = txtTelefono.Text;
            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Dni = txtDNI.Text;

            dataUser.ModificarUsuario(user);
            dataEmpleados.ModificarEmpleado(nuevo);
            return true;
        }
        public bool ModificarCliente()
        {
            var dataEmpleados = new AccesoEmpleados();
            var dataUser = new AccesoUsuario();
            var dataClientes = new AccesoClientes();
            var nuevo = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));

            if (dataEmpleados.Listar().Find(x => x.Dni == txtDNI.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null || dataClientes.Listar().Find(x => x.Dni == txtDNI.Text && x.Usuario.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un usuario con ese DNI...";
                return false;
            }
            else if (dataUser.Listar().Find(x => x.Email == txtEmail.Text && x.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un usuario con ese Email...";
                return false;
            }
            else if (dataClientes.Listar().Find(x => x.Telefono == txtTelefono.Text && x.Usuario.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un cliente con ese Telefono...";
                return false;
            }

            nuevo.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
            nuevo.Dni = txtDNI.Text;
            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.Usuario.Email = txtEmail.Text;

            if (nuevo.Telefono.Length < 8 || nuevo.Telefono.Length > 15)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "El numero de telefono se debe tener entre 8 y 15 digitos...";
                return false;
            }
            dataClientes.ModificarCliente(nuevo);
            return true;
        }
        public void CargarCategoria()
        {
            var datos = new AccesoCategorias();
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