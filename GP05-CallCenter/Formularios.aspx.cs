using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class Formularios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                dgvIncidencias.DataSource = datos.listarIncidenciasEmpleado(emp.IDEmpleado);
                dgvIncidencias.DataBind();
            }
            else if (user.TipoUsuario == TipoUsuario.Admin)
            {
                CommandField aux = new CommandField();
                aux.ShowSelectButton = true;
                aux.SelectText = "Abrir";
                dgvIncidencias.Columns.Add(aux);
                dgvIncidencias.DataSource = datos.listar();
                dgvIncidencias.DataBind();
            }
            else
            {

                AccesoClientes dataCli = new AccesoClientes();
                Cliente cli = dataCli.BuscarClientePorIdUsuario(user.IdUsuario);
                dgvIncidencias.DataSource = datos.listarIncidenciasCliente(cli.IdCliente);
                AgregarDGVCliente();
                dgvIncidencias.DataBind();
            }


        }
        public void AgregarDGVCliente()
        {
            BoundField aux = new BoundField();
            aux.HeaderText = "Descripcion";
            aux.DataField = "Descripcion";
            dgvIncidencias.Columns.Add(aux);
        }

        protected void dgvIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvIncidencias.SelectedDataKey.Value.ToString();
            Response.Redirect("Incidencias.aspx?id=" + id);
        }
    }
}