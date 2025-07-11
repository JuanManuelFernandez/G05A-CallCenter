using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GP05_CallCenter
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
                    Response.Redirect("Error.aspx");
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    //listar incidencias que no tengan IDEmpleado asignado
                    int SinEmpleado = 0;
                    var FiltroIncidencia = datos.Listar().Where(i => i.IdEmpleado == SinEmpleado).ToList();

                    dgvIncidenciasLibres.DataSource = FiltroIncidencia;
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
                int IDIncidencia = Convert.ToInt32(dgvIncidenciasLibres.DataKeys[index].Value);
                AccesoEmpleados  emp = new AccesoEmpleados();
                Usuario user = (Usuario)Session["usuario"];

                if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    int IDEmpleado = emp.ObtenerIDEmpleadoLogueado(user.IdUsuario);

                    AccesoIncidencias datos = new AccesoIncidencias();
                    bool pudoAsignarse = datos.AsignarIncidencia(IDIncidencia, IDEmpleado);

                    if (pudoAsignarse)
                    {
                        Response.Redirect("incidenciasLibres.aspx");
                    }
                }
            }
        }
    }
}