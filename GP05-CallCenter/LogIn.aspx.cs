﻿using System;
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
            if (Session["usuario"]!=null) {
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            AccesoUsuario accesoUsuario = new AccesoUsuario();
            try
            {
                usuario = accesoUsuario.Listar().Find(x => x.Email == txtEmail.Text && x.Clave == txtClave.Text && x.Eliminado == false) != null ? accesoUsuario.Listar().Find(x => x.Email == txtEmail.Text && x.Clave == txtClave.Text) : new Usuario();
                if (accesoUsuario.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Inicio.aspx");
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