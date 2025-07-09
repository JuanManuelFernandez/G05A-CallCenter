using Datos;
using System;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class Edicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblRegistro.Visible = false;
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                // Cliente no puede editar sus datos
                if (user.TipoUsuario == TipoUsuario.Cliente)
                {
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
                if (ModificarCliente()) {
                    Response.Redirect("Admin.aspx");
                } 
            }
            else if (user.TipoUsuario == TipoUsuario.Empleado)
            {
                if (ModificarEmpleado())
                {
                    Response.Redirect("Admin.aspx");
                }
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
        public bool ModificarEmpleado()
        {
            AccesoClientes dataClientes = new AccesoClientes();
            AccesoUsuario dataUser = new AccesoUsuario();
            Usuario user = dataUser.BuscarUsuarioPorId(int.Parse(Request.QueryString["IdUsuario"]));
            AccesoEmpleados dataEmpleados = new AccesoEmpleados();
            Empleado nuevo = dataEmpleados.BuscarPorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));



            if (dataEmpleados.listar().Find(x => x.DNI == txtDNI.Text && x.IDUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null || dataClientes.Listar().Find(x => x.DNI == txtDNI.Text && x.Usuario.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
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
            else if (dataEmpleados.listar().Find(x => x.Legajo == txtTelefono.Text && x.IDUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
            {
                lblRegistro.Visible = true;
                lblRegistro.Text = "Ya existe un empleado con ese Legajo...";
                return false;
            }

            user.Email = txtEmail.Text;
            nuevo.Legajo = txtTelefono.Text;
            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.DNI = txtDNI.Text;

            dataUser.ModificarUsuario(user);
            dataEmpleados.ModificarEmpleado(nuevo);
            return true;
        }
        public bool ModificarCliente()
        {
            AccesoEmpleados dataEmpleados = new AccesoEmpleados();
            AccesoUsuario dataUser = new AccesoUsuario();
            AccesoClientes dataClientes = new AccesoClientes();
            Cliente nuevo = dataClientes.BuscarClientePorIdUsuario(int.Parse(Request.QueryString["IdUsuario"]));

            if (dataEmpleados.listar().Find(x => x.DNI == txtDNI.Text && x.IDUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null || dataClientes.Listar().Find(x => x.DNI == txtDNI.Text && x.Usuario.IdUsuario != int.Parse(Request.QueryString["IdUsuario"])) != null)
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

            nuevo.Categoria.IDCategoria = int.Parse(ddlCategoria.SelectedValue);
            nuevo.DNI = txtDNI.Text;
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