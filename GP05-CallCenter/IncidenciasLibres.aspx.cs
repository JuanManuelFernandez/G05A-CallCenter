using Datos;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace CallCenter
{
    public partial class IndicenciasLibres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var datos = new AccesoIncidencias();
                var user = (Usuario)Session["usuario"];
                if (Session["usuario"] == null)
                {
                    Session.Add("error", "Debes loguearte para acceder a esta pagina");
                    Response.Redirect("error.aspx");
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    //listar incidencia que no tengan IDEmpleado asignado
                    var sinEmpleado = 0;
                    var filtroIncidencia = datos.Listar().Where(i => i.IdEmpleado == sinEmpleado).ToList();

                    dgvIncidenciasLibres.DataSource = filtroIncidencia;
                    dgvIncidenciasLibres.DataBind();
                }
            }
        }

        protected void DgvIncidenciasLibres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Tomar")
            {
                var index = Convert.ToInt32(e.CommandArgument);
                var row = dgvIncidenciasLibres.Rows[index];
                var idIncidencia = Convert.ToInt32(dgvIncidenciasLibres.DataKeys[index].Value);
                var  emp = new AccesoEmpleados();
                var user = (Usuario)Session["usuario"];

                if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    var idEmpleado = emp.ObtenerIdEmpleadoLogueado(user.IdUsuario);

                    var datos = new AccesoIncidencias();
                    var pudoAsignarse = datos.AsignarIncidencia(idIncidencia, idEmpleado);

                    if (pudoAsignarse)
                    {
                        Response.Redirect("incidenciasLibres.aspx");
                    }
                }
            }
        }
    }
}