using Datos;
using GP05_CallCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenter
{
    public partial class Formularios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AccesoIncidencias datos = new AccesoIncidencias();
                if (Session["usuario"] == null)
                {
                    Session.Add("error", "Debes loguearte para acceder a esta pagina");
                    Response.Redirect("Error.aspx");
                }
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    AccesoEmpleados datosEmp = new AccesoEmpleados();
                    Empleado emp = datosEmp.BuscarPorIdUsuario(user.IdUsuario);
                    CommandField aux = new CommandField();
                    aux.ShowSelectButton = true;
                    aux.SelectText = "Abrir";
                    dgvIncidencias.Columns.Add(aux);
                    dgvIncidencias.DataSource = datos.ListarIncidenciasEmpleado(emp.IDEmpleado);
                    dgvIncidencias.DataBind();
                }
                else if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    foreach (DataControlField col in dgvIncidencias.Columns)
                    {
                        if (col.HeaderText == "Asignar Empleado")
                        {
                            col.Visible = true;
                            break;
                        }
                    }
                    CommandField aux = new CommandField();
                    aux.ShowSelectButton = true;
                    aux.SelectText = "Abrir";
                    dgvIncidencias.Columns.Add(aux);
                    dgvIncidencias.DataSource = datos.Listar();
                    dgvIncidencias.DataBind();
                }
                else
                {
                    AccesoClientes dataCli = new AccesoClientes();
                    Cliente cli = dataCli.BuscarClientePorIdUsuario(user.IdUsuario);
                    dgvIncidencias.DataSource = datos.ListarIncidenciasCliente(cli.IdCliente);
                    AgregarDGVCliente();
                    dgvIncidencias.DataBind();
                }
            }
        }
        public void AgregarDGVCliente()
        {
            BoundField aux = new BoundField
            {
                HeaderText = "Descripcion",
                DataField = "Descripcion"
            };
            dgvIncidencias.Columns.Add(aux);
        }
        protected void dgvIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvIncidencias.SelectedDataKey.Value.ToString();
            Response.Redirect("Incidencias.aspx?id=" + id);
        }
        protected void dgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            AccesoEmpleados data = new AccesoEmpleados();
            Incidencia inc = (Incidencia)e.Row.DataItem;
            DropDownList aux = (DropDownList)e.Row.FindControl("ddlEmpleados");
            aux.DataSource = data.listar();
            aux.DataValueField = "IDEmpleado";
            aux.DataTextField = "Legajo";
            aux.DataBind();
            if (inc.IdEmpleado != 0) aux.SelectedValue = inc.IdEmpleado.ToString();
            else
            {
                aux.Items.Insert(0, new ListItem("Asignar"));
                aux.SelectedIndex = 0;
            }
        }

        protected void ddlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList lista = (DropDownList)sender;
            GridViewRow fila = (GridViewRow)lista.NamingContainer;
            AccesoIncidencias data = new AccesoIncidencias();
            Incidencia inc = data.Listar().Find(x => x.IdIncidencia == int.Parse(dgvIncidencias.DataKeys[fila.RowIndex].Value.ToString()));
            inc.IdEmpleado = int.Parse(lista.SelectedValue);
            inc.Resolucion = string.Empty;
            data.ModificarIncidencia(inc);
        }
    }
}