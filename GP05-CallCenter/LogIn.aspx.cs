using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace CallCenter
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            AccesoUsuario accesoUsuario = new AccesoUsuario();
            //usuario = new Usuario(3, txtEmail.Text, txtClave.Text);
            try
            {
                usuario = new Usuario(3, txtEmail.Text, txtClave.Text); // Hardcodeado como Cliente (menos privilegios), aunque se pisa en Loguear
                if (accesoUsuario.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    if (usuario.TipoUsuario == TipoUsuario.Admin)
                    {
                        //Response.Redirect("Dashboard.aspx"); Panel de Administrador (To-Do)
                    }
                    else if (usuario.TipoUsuario == TipoUsuario.Empleado)
                    {
                        Response.Redirect("Incidencias.aspx");
                    }
                    else if (usuario.TipoUsuario == TipoUsuario.Cliente)
                    {
                        Response.Redirect("Formularios.aspx");
                    }
                } 
                else
                {
                    //Session.Add("error", "Email o Clave incorrectos");
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}