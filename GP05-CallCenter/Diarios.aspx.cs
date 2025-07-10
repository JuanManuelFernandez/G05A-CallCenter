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
                    AccesoUsuario datosUsers = new AccesoUsuario();
                    dgvEmpleados.DataSource = datosUsers.ListarUsuarios();
                    dgvEmpleados.DataBind();
                    return;
                }
            }
            Response.Redirect("Inicio.aspx");

            //EmailService emailService = new EmailService();

            //emailService.ArmarCorreo(txtMail.Text, "Se genero la Incidencia Numero " + IDIncidencia, "Descripcion: <br>" + txtDescripcion.Text);
        }
        protected void DgvEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //emailService.EnviarEmail();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }

        }
    }
}