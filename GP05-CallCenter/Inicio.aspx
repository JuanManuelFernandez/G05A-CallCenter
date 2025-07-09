<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GP05_CallCenter.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-3">
        <div class="col-md-4 text-center">
            <h1 class="py-3 mb-4 bg-primary text-white display-4">Bienvenido/a <%= NombreDeUsuario %></h1>
            <h2>Elige una opción:</h2>
            <br />

            <div class="d-grid gap-3">

                <asp:LinkButton ID="btnMisReclamos" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" OnClick="BtnReclamos_Click">
                <img src="Assets/Lista.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Mis reclamos
                </asp:LinkButton>

                <asp:LinkButton ID="btnCargar" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" OnClick="BtnCargar_Click">
                <img src="Assets/Cargar.png" style="width: 30px; height: 30px; margin-right: 20px;" />
                Cargar Reclamo
                </asp:LinkButton>

                <asp:LinkButton ID="btnMisDatos" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" OnClick="BtnDatos_Click">
                <img src="Assets/Datos.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Mis datos
                </asp:LinkButton>

                <asp:LinkButton ID="btnModificarTipos" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" Visible="false" OnClick="BtnModificarTipos_Click">
                <img src="Assets/Lista.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Modificar Tipos
                </asp:LinkButton>

                <asp:LinkButton ID="btnIncidenciasLibres" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" Visible="false" OnClick="BtnIncidenciasLibres_Click">
                <img src="Assets/InciLibres.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Incidencias libres
                </asp:LinkButton>

                <asp:LinkButton ID="btnRegistrarCliente" runat="server" CssClass="btn btn-success btn-lg d-flex justify-content-center align-items-center" Visible="false" OnClick="BtnDarDeAltaUsuario_Click">
                <img src="Assets/RegisterU.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Agregar usuario
                </asp:LinkButton>

            </div>
        </div>
    </div>


</asp:Content>
