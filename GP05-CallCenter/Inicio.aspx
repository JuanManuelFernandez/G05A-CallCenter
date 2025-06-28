<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GP05_CallCenter.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-3">
        <div class="col-md-6 text-center">
            <h1 class="py-3 mb-4 bg-primary text-white display-4" style="display: inline-block;">Bienvenido/a <%= NombreDeUsuario %></h1>
            <p>Elige una opcion:</p>
        </div>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnMisDatos" runat="server" Text="Mis datos" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnDatos_Click" />
        </section>
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnCargar" runat="server" Text="Cargar Reclamo" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnCargar_Click" />
        </section>
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnMisReclamos" runat="server" Text="Mis reclamos" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnReclamos_Click" />
        </section>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnModificarTipos" runat="server" Text="Modificar Tipos" CssClass="btn btn-primary btn-lg mx-3" Visible ="false" OnClick="btnModificarTipos_Click"/>
        </section>
    </div>

</asp:Content>
