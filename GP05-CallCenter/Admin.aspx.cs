﻿using Datos;
using System;

namespace GP05_CallCenter
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                if (user.TipoUsuario == TipoUsuario.Admin)
                {
                    AccesoUsuario datos = new AccesoUsuario();
                    dgvUsuarios.DataSource = datos.ListarUsuarios();
                    dgvUsuarios.DataBind();
                    return;
                }
            }
            Response.Redirect("Inicio.aspx");
        }

        protected void DgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("Edicion.aspx?IdUsuario=" + id);
        }
    }
}