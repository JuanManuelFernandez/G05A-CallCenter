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
                var datos = new AccesoIncidencias();
                if (Session["usuario"] == null)
                {
                    Session.Add("error", "Debes loguearte para acceder a esta pagina");
                    Response.Redirect("error.aspx");
                }
                var user = (Usuario)Session["usuario"];
                switch (user.TipoUsuario)
                {
                    //Pagina para...
                    // Admin
                    case TipoUsuario.Admin:
                    {
                        foreach (DataControlField col in dgvIncidencias.Columns)
                        {
                            if (col.HeaderText == "Asignar Empleado")
                            {
                                col.Visible = true;
                                break;
                            }
                        }
                        var aux = new CommandField
                        {
                            ShowSelectButton = true,
                            SelectText = "Abrir"
                        };
                        dgvIncidencias.Columns.Add(aux);
                        Session.Add("listaCasos", datos.Listar());
                        dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                        break;
                    }
                    //Empleado
                    case TipoUsuario.Empleado:
                    {
                        var datosEmp = new AccesoEmpleados();
                        var emp = datosEmp.BuscarPorIdUsuario(user.IdUsuario);
                        var aux = new CommandField
                        {
                            ShowSelectButton = true,
                            SelectText = "Abrir"
                        };
                        dgvIncidencias.Columns.Add(aux);
                        Session.Add("listaCasos", datos.ListarIncidenciasEmpleado(emp.IdEmpleado));
                        dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                        break;
                    }
                    //Cliente
                    default:
                    {
                        var dataCli = new AccesoClientes();
                        var cli = dataCli.BuscarClientePorIdUsuario(user.IdUsuario);
                        Session.Add("listaCasos", datos.ListarIncidenciasCliente(cli.IdCliente));
                        dgvIncidencias.DataSource = Session["listaCasos"]; // Capturo lista en Session
                        AgregarDgvCliente();
                        break;
                    }
                }
                dgvIncidencias.DataBind();

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

            switch (ddlTipo.SelectedItem.Text)
            {
                case "Seleccionar...":
                    ddlPrioridad.Visible = false;
                    txtBuscar.Visible = false;
                    btnBuscar.Enabled = false;
                    break;
                case "Prioridad":
                    ddlPrioridad.Visible = true;
                    txtBuscar.Visible = false;
                    btnBuscar.Enabled = false;
                    ddlPrioridad.Items.Add("Cualquiera");
                    ddlPrioridad.Items.Add("Alta");
                    ddlPrioridad.Items.Add("Media");
                    ddlPrioridad.Items.Add("Baja");
                    break;
                case "Estado Actual":
                    ddlPrioridad.Visible = false;
                    txtBuscar.Visible = true;
                    btnBuscar.Enabled = true;
                    break;
            }
        }
        public void DdlPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lista = (List<Incidencias>)Session["listaCasos"];
            if (ddlPrioridad.SelectedValue == "Cualquiera")
            {
                dgvIncidencias.DataSource = lista;
            }
            else
            {
                var listaFiltrada = lista.FindAll(x => x.Prioridad.Nombre == ddlPrioridad.SelectedValue);
                dgvIncidencias.DataSource = listaFiltrada;
            }
            dgvIncidencias.DataBind();
        }
        public void BtnBuscar_Click(object sender, EventArgs e)
        {
            var lista = (List<Incidencias>)Session["listaCasos"];
            var listaFiltrada = lista.FindAll(x => x.EstadoActual.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            dgvIncidencias.DataSource = listaFiltrada;
            dgvIncidencias.DataBind();
        }
        public void AgregarDgvCliente()
        {
            var aux = new BoundField
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
            var data = new AccesoEmpleados();
            var inc = (Incidencias)e.Row.DataItem;
            var aux = (DropDownList)e.Row.FindControl("ddlEmpleados");
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
            aux.Enabled = string.IsNullOrEmpty(inc.Resolucion);
        }
        protected void DdlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lista = (DropDownList)sender;
            var fila = (GridViewRow)lista.NamingContainer;
            var data = new AccesoIncidencias();
            var inc = data.Listar().Find(x =>
                x.IdIncidencia == int.Parse(dgvIncidencias.DataKeys[fila.RowIndex].Value.ToString()));
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
                var index = Convert.ToInt32(e.CommandArgument);
                var id = dgvIncidencias.DataKeys[index]?.Value.ToString();
                Response.Redirect("historial.aspx?id=" + id);
            }
        }
    }
}