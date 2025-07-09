<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GP05_CallCenter.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-3">
        <div class="col-md-4 text-center">
            <h1 class="py-3 mb-4 bg-primary text-white display-4">Bienvenido/a <%= NombreDeUsuario %></h1>
            <h2>Elige una opción:</h2> <br/>

            <!-- Botones verticales -->
            <div class="d-grid gap-3">

                <asp:Button ID="btnMisDatos" runat="server" Text="Mis datos" CssClass="btn btn-primary btn-lg" OnClick="btnDatos_Click" />

                <asp:Button ID="btnCargar" runat="server" Text="Cargar Reclamo" CssClass="btn btn-primary btn-lg" OnClick="btnCargar_Click" />

                <asp:Button ID="btnMisReclamos" runat="server" Text="Mis reclamos" CssClass="btn btn-primary btn-lg" OnClick="btnReclamos_Click" />

                <asp:Button ID="btnModificarTipos" runat="server" Text="Modificar Tipos" CssClass="btn btn-primary btn-lg" Visible="false" OnClick="btnModificarTipos_Click" />

                <asp:LinkButton ID="btnIncidenciasLibres" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" Visible="false" OnClick="btnIncidenciasLibres_Click">
                <img src="Assets/InciLibres.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Incidencias libres
                </asp:LinkButton>

                <asp:LinkButton ID="btnRegistrarCliente" runat="server" CssClass="btn btn-success btn-lg d-flex justify-content-center align-items-center" Visible="false" OnClick="btnDarDeAltaUsuario_Click">
                <img src="Assets/RegisterU.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                Agregar usuario
                </asp:LinkButton>

            </div>
        </div>
    </div>


</asp:Content>
