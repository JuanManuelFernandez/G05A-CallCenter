using System;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace GP05_CallCenter
{
    public partial class Diarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    if (!IsPostBack)
                    {
                        AccesoUsuario datosUsers = new AccesoUsuario();
                        dgvEmpleados.DataSource = datosUsers.ListarEmpleados();
                        dgvEmpleados.DataBind();
                    }
                    return;
                }
            }
            Response.Redirect("Inicio.aspx");


        }
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            AccesoIncidencias datosInc = new AccesoIncidencias();
            AccesoEmpleados datosEmp = new AccesoEmpleados();
            AccesoUsuario datosUsr = new AccesoUsuario();
            EmailService emailService = new EmailService();

            foreach (GridViewRow row in dgvEmpleados.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkEnviar");
                if (chk != null && chk.Checked)
                {
                    int alta = 0;
                    int media = 0;
                    int baja = 0;
                    string emailBody = string.Empty;

                    int idUsuario = Convert.ToInt32(dgvEmpleados.DataKeys[row.RowIndex].Value);
                    Usuario usr = datosUsr.BuscarUsuarioPorId(idUsuario); // Acceso a Email
                    Empleado emp = datosEmp.BuscarPorIdUsuario(idUsuario);
                    foreach (Incidencia inc in datosInc.Listar())
                    {
                        if (inc.IdEmpleado == emp.IDEmpleado)
                        {
                            // Calculo de Estadisticas...
                            switch (inc.prioridad.IDPrioridad)
                            {
                                case 1: alta++; break;
                                case 2: media++; break;
                                case 3: baja++; break;
                            }

                            // Incidencia 1 - Fecha - Esatdo actual - Descripcion
                            // Incidencia 2 - Fecha - Esatdo actual - Descripcion
                            // Incidencia N...
                            emailBody +=
                                "<h1>Incidencia #" + inc.IdIncidencia + ":</h1>" +
                                "<b>Fecha/hora creacion:</b> " + inc.FechaYHoraCreacion + "<br><br>" +
                                "<b>Estado actual:</b>  <br>" +
                                inc.EstadoActual + "<br><br>" +
                                "<b>Descripcion:</b> <br>" +
                                inc.Descripcion + "<br><br>";
                        }
                    }

                    // Cargo Estadisticas al final del mail
                    emailBody +=
                        "<hr>" +
                        "<h2>Estadísticas:</h2>" +
                        "<h3>Prioridad:</h3>" +
                        $"<strong>Alta:</strong> {alta}<br>" +
                        $"<strong>Media:</strong> {media}<br>" +
                        $"<strong>Baja:</strong> {baja}<br>" +
                        $"<h3>Total: {alta+media+baja} </h3>";

                    // Armo el email aca...
                    emailService.ArmarCorreo
                    (
                        usr.Email,
                        "Reporte Mensual de Incidencias - Empleado #" + emp.IDEmpleado,
                        emailBody
                    );
                    // Y lo mando
                    try
                    {
                        emailService.EnviarEmail();
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex);
                        Response.Redirect("Error.aspx");
                    }

                    Response.Redirect("Inicio.aspx");
                }
            }

            // Mostrar mensaje de éxito..?
        }
    }
}