﻿using Datos;
using System;
using System.Web.UI;

namespace CallCenter
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                var user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    Response.Redirect("inicio.aspx");
                }
                else if (user.TipoUsuario == TipoUsuario.Empleado)
                {
                    Response.Redirect("inicio.aspx");
                }
                else
                {
                    Response.Redirect("inicio.aspx");
                }
            }
        }
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("registro.aspx");
        }
        protected void BtnLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}