using Datos;
using System;

namespace GP05_CallCenter
{
    public partial class EdicionDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblRegistro.Visible = false;
                if (Session["usuario"] == null || ((Usuario)Session["usuario"]).TipoUsuario != TipoUsuario.Admin) // Si no hay sesion de usuario o TipoUsuario != Admin
                {
                    Response.Redirect("Inicio.aspx");
                }
                ddlTipo.Items.Add("Categorias Cliente");
                ddlTipo.Items.Add("Tipos Incidente");
                ddlTipo.Items.Add("Prioridades Incidente");
                ddlElegir.Items.Add("Alta");
                ddlElegir.Items.Add("Modificar");
                ddlElegir.Items.Add("Baja");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlElegir.SelectedItem.Text == "Alta")
            {
                Cargar();
            }
            else if (ddlElegir.SelectedItem.Text == "Baja")
            {
                Eliminar();
            }
            else if (ddlElegir.SelectedItem.Text == "Modificar")
            {
                Modificar();
            }

        }
        public void Cargar()
        {
            if (ddlTipo.SelectedItem.Text == "Categorias Cliente")
            {
                AccesoCategorias datos = new AccesoCategorias();
                CategoriasCliente nuevo = new CategoriasCliente();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                {
                    lblRegistro.Visible = true;
                }
                else
                {
                    datos.AgregarCategoria(nuevo);
                    Response.Redirect("Inicio.aspx");
                }
            }
            else if (ddlTipo.SelectedItem.Text == "Tipos Incidente")
            {
                AccesoTipos datos = new AccesoTipos();
                TiposIncidente nuevo = new TiposIncidente();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                {
                    lblRegistro.Visible = true;
                }
                else
                {
                    datos.AgregarTipos(nuevo);
                    Response.Redirect("Inicio.aspx");
                }
            }
            else if (ddlTipo.SelectedItem.Text == "Prioridades Incidente")
            {
                AccesoPrioridades datos = new AccesoPrioridades();
                PrioridadesIncidente nuevo = new PrioridadesIncidente();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                {
                    lblRegistro.Visible = true;
                }
                else
                {
                    datos.AgregarPrioridades(nuevo);
                    Response.Redirect("Inicio.aspx");
                }
            }
        }
        public void Eliminar()
        {
            if (ddlTipo.SelectedItem.Text == "Categorias Cliente")
            {
                AccesoCategorias datos = new AccesoCategorias();
                CategoriasCliente nuevo = new CategoriasCliente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDCategoria = int.Parse(ddlDato.SelectedValue);
                datos.EliminarCategoria(nuevo);
                Response.Redirect("Inicio.aspx");
            }
            else if (ddlTipo.SelectedItem.Text == "Tipos Incidente")
            {
                AccesoTipos datos = new AccesoTipos();
                TiposIncidente nuevo = new TiposIncidente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDTipo = int.Parse(ddlDato.SelectedValue);
                datos.EliminarTipos(nuevo);
                Response.Redirect("Inicio.aspx");
            }
            else if (ddlTipo.SelectedItem.Text == "Prioridades Incidente")
            {
                AccesoPrioridades datos = new AccesoPrioridades();
                PrioridadesIncidente nuevo = new PrioridadesIncidente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDPrioridad = int.Parse(ddlDato.SelectedValue);
                datos.EliminarPrioridades(nuevo);
                Response.Redirect("Inicio.aspx");
            }
        }
        public void Modificar()
        {
            if (ddlTipo.SelectedItem.Text == "Categorias Cliente")
            {
                AccesoCategorias datos = new AccesoCategorias();
                CategoriasCliente nuevo = new CategoriasCliente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDCategoria = int.Parse(ddlDato.SelectedValue);
                datos.ModificarCategoria(nuevo);
                Response.Redirect("Inicio.aspx");
            }
            else if (ddlTipo.SelectedItem.Text == "Tipos Incidente")
            {
                AccesoTipos datos = new AccesoTipos();
                TiposIncidente nuevo = new TiposIncidente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDTipo = int.Parse(ddlDato.SelectedValue);
                datos.ModificarTipos(nuevo);
                Response.Redirect("Inicio.aspx");
            }
            else if (ddlTipo.SelectedItem.Text == "Prioridades Incidente")
            {
                AccesoPrioridades datos = new AccesoPrioridades();
                PrioridadesIncidente nuevo = new PrioridadesIncidente();
                nuevo.Nombre = ddlDato.SelectedItem.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.IDPrioridad = int.Parse(ddlDato.SelectedValue);
                datos.ModificarPrioridades(nuevo);
                Response.Redirect("Inicio.aspx");
            }
        }
        public void ListarCategoria()
        {
            AccesoCategorias data = new AccesoCategorias(); ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDCategoria";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void ListarTipos()
        {
            AccesoTipos data = new AccesoTipos();
            ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDTipo";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void ListarPrioridades()
        {
            AccesoPrioridades data = new AccesoPrioridades();
            ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDPrioridad";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void Validar()
        {
            if (ddlTipo.SelectedItem.Text == "Categorias Cliente")
            {
                ListarCategoria();
            }
            else if (ddlTipo.SelectedItem.Text == "Tipos Incidente")
            {
                ListarTipos();
            }
            else if (ddlTipo.SelectedItem.Text == "Prioridades Incidente")
            {
                ListarPrioridades();
            }
        }
        public void AutoCompletar()
        {
            if (ddlTipo.SelectedItem.Text == "Categorias Cliente")
            {
                AccesoCategorias data = new AccesoCategorias();
                txtDescripcion.Text = (data.Listar().Find(x => x.IDCategoria == int.Parse(ddlDato.SelectedValue))).Descripcion;
            }
            else if (ddlTipo.SelectedItem.Text == "Tipos Incidente")
            {
                AccesoTipos data = new AccesoTipos();
                txtDescripcion.Text = (data.Listar().Find(x => x.IDTipo == int.Parse(ddlDato.SelectedValue))).Descripcion;
            }
            else if (ddlTipo.SelectedItem.Text == "Prioridades Incidente")
            {
                AccesoPrioridades data = new AccesoPrioridades();
                txtDescripcion.Text = (data.Listar().Find(x => x.IDPrioridad == int.Parse(ddlDato.SelectedValue))).Descripcion;
            }
        }
        protected void ddlElegir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlElegir.SelectedItem.Text == "Alta")
            {
                txtNombre.Visible = true;
                ddlDato.Visible = false;
                txtDescripcion.Enabled = true;
                btnAgregar.Text = "Agregar";
                btnAgregar.CssClass = "btn btn-success btn-lg";
                Validar();
                txtDescripcion.Text = string.Empty;
            }
            else if (ddlElegir.SelectedItem.Text == "Modificar")
            {
                txtNombre.Visible = false;
                ddlDato.Visible = true;
                txtDescripcion.Enabled = true;
                btnAgregar.Text = "Modificar";
                btnAgregar.CssClass = "btn btn-primary btn-lg";
                Validar();
            }
            else if (ddlElegir.SelectedItem.Text == "Baja")
            {
                txtNombre.Visible = false;
                ddlDato.Visible = true;
                txtDescripcion.Enabled = false;
                btnAgregar.Text = "Eliminar";
                btnAgregar.CssClass = "btn btn-danger btn-lg";
                Validar();
            }

        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlElegir.SelectedItem.Text != "Alta")
            {
                Validar();
            }
        }

        protected void ddlDato_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompletar();
        }
    }
}