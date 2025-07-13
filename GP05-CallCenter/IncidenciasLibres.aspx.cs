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
                AccesoIncidencias datos = new AccesoIncidencias();
                Usuario user = (Usuario)Session["usuario"];
                if (Session["usuario"] == null)
                {
                    Session.Add("error", "Debes loguearte para acceder a esta pagina");
                    Response.Redirect("error.aspx");
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    //listar incidencia que no tengan IDEmpleado asignado
                    int sinEmpleado = 0;
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
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = dgvIncidenciasLibres.Rows[index];
                int idIncidencia = Convert.ToInt32(dgvIncidenciasLibres.DataKeys[index].Value);
                AccesoEmpleados  emp = new AccesoEmpleados();
                Usuario user = (Usuario)Session["usuario"];

                if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    int idEmpleado = emp.ObtenerIdEmpleadoLogueado(user.IdUsuario);

                    AccesoIncidencias datos = new AccesoIncidencias();
                    bool pudoAsignarse = datos.AsignarIncidencia(idIncidencia, idEmpleado);

                    if (pudoAsignarse)
                    {
                        Response.Redirect("incidenciasLibres.aspx");
                    }
                }
            }
        }
    }
}