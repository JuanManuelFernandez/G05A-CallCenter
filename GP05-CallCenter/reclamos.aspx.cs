using Datos;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CallCenter
{
    public partial class Reclamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AccesoIncidencias datos = new AccesoIncidencias();
                if (Session["usuario"] == null)
                {
                    Session.Add("error", "Debes loguearte para acceder a esta pagina");
                    Response.Redirect("error.aspx");
                }
                Usuario user = (Usuario)Session["usuario"];
                //Pagina para...
                // admin
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    foreach (DataControlField col in dgvIncidencias.Columns)
                    {
                        if (col.HeaderText == "Asignar Empleado")
                        {
                            col.Visible = true;
                            break;
                        }
                    }
                    CommandField aux = new CommandField
                    {
                        ShowSelectButton = true,
                        SelectText = "Abrir"
                    };
                    dgvIncidencias.Columns.Add(aux);
                    Session.Add("listaCasos", datos.Listar());
                    dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                    dgvIncidencias.DataBind();
                }
                //Empleado
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    AccesoEmpleados datosEmp = new AccesoEmpleados();
                    Empleado emp = datosEmp.BuscarPorIdUsuario(user.IdUsuario);
                    CommandField aux = new CommandField
                    {
                        ShowSelectButton = true,
                        SelectText = "Abrir"
                    };
                    dgvIncidencias.Columns.Add(aux);
                    Session.Add("listaCasos", datos.ListarIncidenciasEmpleado(emp.IdEmpleado));
                    dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                    dgvIncidencias.DataBind();
                }
                //Cliente
                else
                {
                    AccesoClientes dataCli = new AccesoClientes();
                    Cliente cli = dataCli.BuscarClientePorIdUsuario(user.IdUsuario);
                    Session.Add("listaCasos", datos.ListarIncidenciasCliente(cli.IdCliente));
                    dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                    AgregarDgvCliente();
                    dgvIncidencias.DataBind();
                }
                ddlTipo.Items.Add("Seleccionar...");
                ddlTipo.Items.Add("Prioridad");
                ddlTipo.Items.Add("Estado Actual");
                txtBuscar.Visible = false;
                btnBuscar.Enabled = false;
                //ddlTipo.Items.Add("Fecha/Hora Creacion\t");
            }
        }
        public void DdlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPrioridad.Items.Clear();

            if (ddlTipo.SelectedItem.Text == "Seleccionar...")
            {
                ddlPrioridad.Visible = false;
                txtBuscar.Visible = false;
                btnBuscar.Enabled = false;
            }
            if (ddlTipo.SelectedItem.Text == "Prioridad")
            {
                ddlPrioridad.Visible = true;
                txtBuscar.Visible = false;
                btnBuscar.Enabled = false;
                ddlPrioridad.Items.Add("Cualquiera");
                ddlPrioridad.Items.Add("Alta");
                ddlPrioridad.Items.Add("Media");
                ddlPrioridad.Items.Add("Baja");
            }
            else if (ddlTipo.SelectedItem.Text == "Estado Actual")
            {
                ddlPrioridad.Visible = false;
                txtBuscar.Visible = true;
                btnBuscar.Enabled = true;
            }
        }
        public void DdlPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Incidencias> lista = (List<Incidencias>)Session["listaCasos"];
            if (ddlPrioridad.SelectedValue == "Cualquiera")
            {
                dgvIncidencias.DataSource = lista;
            }
            else
            {
                List<Incidencias> listaFiltrada = lista.FindAll(x => x.Prioridad.Nombre == ddlPrioridad.SelectedValue);
                dgvIncidencias.DataSource = listaFiltrada;
            }
            dgvIncidencias.DataBind();
        }
        public void BtnBuscar_Click(object sender, EventArgs e)
        {
            List<Incidencias> lista = (List<Incidencias>)Session["listaCasos"];
            List<Incidencias> listaFiltrada = lista.FindAll(x => x.EstadoActual.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvIncidencias.DataSource = listaFiltrada;
            dgvIncidencias.DataBind();
        }
        public void AgregarDgvCliente()
        {
            BoundField aux = new BoundField
            {
                HeaderText = "Descripcion",
                DataField = "Descripcion"
            };
            dgvIncidencias.Columns.Add(aux);
        }
        protected void DgvIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvIncidencias.SelectedDataKey.Value.ToString();
            Response.Redirect("incidencia.aspx?id=" + id);
        }
        protected void DgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            AccesoEmpleados data = new AccesoEmpleados();
            Incidencias inc = (Incidencias)e.Row.DataItem;
            DropDownList aux = (DropDownList)e.Row.FindControl("ddlEmpleados");
            aux.DataSource = data.Listar();
            aux.DataValueField = "IDEmpleado";
            aux.DataTextField = "Legajo";
            aux.DataBind();
            if (inc.IdEmpleado != 0) aux.SelectedValue = inc.IdEmpleado.ToString();
            else
            {
                aux.Items.Insert(0, new ListItem("Asignar"));
                aux.SelectedIndex = 0;
            }
            if (!(string.IsNullOrEmpty(inc.Resolucion)))
            {
                aux.Enabled = false;
            }
            else
            {
                aux.Enabled = true;
            }
        }
        protected void DdlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList lista = (DropDownList)sender;
            GridViewRow fila = (GridViewRow)lista.NamingContainer;
            AccesoIncidencias data = new AccesoIncidencias();
            Incidencias inc = data.Listar().Find(x => x.IdIncidencia == int.Parse(dgvIncidencias.DataKeys[fila.RowIndex].Value.ToString()));
            if (lista.SelectedItem.Text == "Asignar") return;
            inc.IdEmpleado = int.Parse(lista.SelectedValue);
            inc.Resolucion = string.Empty;
            inc.EstadoActual = "Asignado";
            data.ModificarIncidencia(inc);
        }
        protected void DgvIncidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Abrir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var id = dgvIncidencias.DataKeys[index].Value.ToString();
                Response.Redirect("historial.aspx?id=" + id);
            }
        }
    }
}