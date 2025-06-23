using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
{
    public partial class EdicionDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            else if (((Usuario)Session["usuario"]).TipoUsuario != TipoUsuario.Admin)
            {
                Response.Redirect("Inicio.aspx");
            }
            ddlTipo.Items.Add("Categorias Cliente");
            ddlTipo.Items.Add("Tipos Incidente");
            ddlTipo.Items.Add("Proridades Incidente");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
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
                }
                Response.Redirect("Inicio.aspx");
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
                }
                Response.Redirect("Inicio.aspx");
            }
            else if (ddlTipo.SelectedItem.Text == "Proridades Incidente")
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
                }
                Response.Redirect("Inicio.aspx");
            }
        }
    }
}