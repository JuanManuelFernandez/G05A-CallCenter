using System;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace CallCenter
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
            Response.Redirect("inicio.aspx");
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
                    foreach (Incidencias inc in datosInc.Listar())
                    {
                        if (inc.IdEmpleado == emp.IdEmpleado)
                        {
                            // Calculo de Estadisticas...
                            switch (inc.Prioridad.IdPrioridad)
                            {
                                case 1: alta++; break;
                                case 2: media++; break;
                                case 3: baja++; break;
                            }

                            // Incidencias 1 - Fecha - Esatdo actual - Descripcion
                            // Incidencias 2 - Fecha - Esatdo actual - Descripcion
                            // Incidencias N...
                            emailBody +=
                                "<h1>Incidencias #" + inc.IdIncidencia + ":</h1>" +
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
                        $"<h3>Total: {alta + media + baja} </h3>";

                    // Armo el email...
                    emailService.ArmarCorreo
                    (
                        usr.Email,
                        "Reporte Mensual de incidencia - Empleado #" + emp.IdEmpleado,
                        emailBody
                    );
                    // ...y lo envio
                    try
                    {
                        emailService.EnviarEmail();
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex);
                        Response.Redirect("error.aspx");
                    }

                    Response.Redirect("inicio.aspx");
                }
            }

            // Mostrar mensaje de éxito..?
        }
    }
}