using Datos;
using System;

namespace CallCenter
{
    public partial class EditarDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblRegistro.Visible = false;
                if (Session["usuario"] == null || ((Usuario)Session["usuario"]).TipoUsuario != TipoUsuario.Admin) // Si no hay sesion de usuario o TipoUsuario != admin
                {
                    Response.Redirect("inicio.aspx");
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
            switch (ddlElegir.SelectedItem.Text)
            {
                case "Alta":
                    Cargar();
                    break;
                case "Baja":
                    Eliminar();
                    break;
                case "Modificar":
                    Modificar();
                    break;
            }
        }
        public void Cargar()
        {
            switch (ddlTipo.SelectedItem.Text)
            {
                case "Categorias Cliente":
                {
                    var datos = new AccesoCategorias();
                    var nuevo = new CategoriasCliente
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text
                    };
                    if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                    {
                        lblRegistro.Visible = true;
                    }
                    else
                    {
                        datos.AgregarCategoria(nuevo);
                        Response.Redirect("inicio.aspx");
                    }

                    break;
                }
                case "Tipos Incidente":
                {
                    var datos = new AccesoTipos();
                    var nuevo = new TiposIncidente
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text
                    };
                    if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                    {
                        lblRegistro.Visible = true;
                    }
                    else
                    {
                        datos.AgregarTipos(nuevo);
                        Response.Redirect("inicio.aspx");
                    }

                    break;
                }
                case "Prioridades Incidente":
                {
                    var datos = new AccesoPrioridades();
                    var nuevo = new PrioridadesIncidente
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text
                    };
                    if (datos.Listar().Find(x => x.Nombre == nuevo.Nombre) != null)
                    {
                        lblRegistro.Visible = true;
                    }
                    else
                    {
                        datos.AgregarPrioridades(nuevo);
                        Response.Redirect("inicio.aspx");
                    }

                    break;
                }
            }
        }
        public void Eliminar()
        {
            switch (ddlTipo.SelectedItem.Text)
            {
                case "Categorias Cliente":
                {
                    var datos = new AccesoCategorias();
                    var nuevo = new CategoriasCliente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdCategoria = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.EliminarCategoria(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
                case "Tipos Incidente":
                {
                    var datos = new AccesoTipos();
                    var nuevo = new TiposIncidente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdTipo = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.EliminarTipos(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
                case "Prioridades Incidente":
                {
                    var datos = new AccesoPrioridades();
                    var nuevo = new PrioridadesIncidente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdPrioridad = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.EliminarPrioridades(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
            }
        }
        public void Modificar()
        {
            switch (ddlTipo.SelectedItem.Text)
            {
                case "Categorias Cliente":
                {
                    var datos = new AccesoCategorias();
                    var nuevo = new CategoriasCliente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdCategoria = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.ModificarCategoria(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
                case "Tipos Incidente":
                {
                    var datos = new AccesoTipos();
                    var nuevo = new TiposIncidente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdTipo = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.ModificarTipos(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
                case "Prioridades Incidente":
                {
                    var datos = new AccesoPrioridades();
                    var nuevo = new PrioridadesIncidente
                    {
                        Nombre = ddlDato.SelectedItem.Text,
                        Descripcion = txtDescripcion.Text,
                        IdPrioridad = int.Parse(ddlDato.SelectedValue)
                    };
                    datos.ModificarPrioridades(nuevo);
                    Response.Redirect("inicio.aspx");
                    break;
                }
            }
        }
        public void ListarCategoria()
        {
            var data = new AccesoCategorias(); ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDCategoria";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void ListarTipos()
        {
            var data = new AccesoTipos();
            ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDTipo";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void ListarPrioridades()
        {
            var data = new AccesoPrioridades();
            ddlDato.DataSource = data.Listar();
            ddlDato.DataValueField = "IDPrioridad";
            ddlDato.DataTextField = "Nombre";
            ddlDato.DataBind();
            AutoCompletar();
        }
        public void Validar()
        {
            switch (ddlTipo.SelectedItem.Text)
            {
                case "Categorias Cliente":
                    ListarCategoria();
                    break;
                case "Tipos Incidente":
                    ListarTipos();
                    break;
                case "Prioridades Incidente":
                    ListarPrioridades();
                    break;
            }
        }
        public void AutoCompletar()
        {
            switch (ddlTipo.SelectedItem.Text)
            {
                case "Categorias Cliente":
                {
                    var data = new AccesoCategorias();
                    txtDescripcion.Text = (data.Listar().Find(x => x.IdCategoria == int.Parse(ddlDato.SelectedValue))).Descripcion;
                    break;
                }
                case "Tipos Incidente":
                {
                    var data = new AccesoTipos();
                    txtDescripcion.Text = (data.Listar().Find(x => x.IdTipo == int.Parse(ddlDato.SelectedValue))).Descripcion;
                    break;
                }
                case "Prioridades Incidente":
                {
                    var data = new AccesoPrioridades();
                    txtDescripcion.Text = (data.Listar().Find(x => x.IdPrioridad == int.Parse(ddlDato.SelectedValue))).Descripcion;
                    break;
                }
            }
        }
        protected void ddlElegir_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlElegir.SelectedItem.Text)
            {
                case "Alta":
                    txtNombre.Visible = true;
                    ddlDato.Visible = false;
                    txtDescripcion.Enabled = true;
                    btnAgregar.Text = "Agregar";
                    btnAgregar.CssClass = "btn btn-success btn-lg";
                    Validar();
                    txtDescripcion.Text = string.Empty;
                    break;
                case "Modificar":
                    txtNombre.Visible = false;
                    ddlDato.Visible = true;
                    txtDescripcion.Enabled = true;
                    btnAgregar.Text = "Modificar";
                    btnAgregar.CssClass = "btn btn-primary btn-lg";
                    Validar();
                    break;
                case "Baja":
                    txtNombre.Visible = false;
                    ddlDato.Visible = true;
                    txtDescripcion.Enabled = false;
                    btnAgregar.Text = "Eliminar";
                    btnAgregar.CssClass = "btn btn-danger btn-lg";
                    Validar();
                    break;
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