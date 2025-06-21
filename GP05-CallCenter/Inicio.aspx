<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GP05_CallCenter.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="text-center">Bienvenido [nombre de usuario]</h1>
    <p class="text-center">Elige una opcion:</p>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3" aria-labelledby="hostingTitle">
            <asp:Button ID="btnMisDatos" runat="server" Text="Mis datos" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnDatos_Click"/>
        </section>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3" aria-labelledby="hostingTitle">
            <asp:Button ID="btnMisReclamos" runat="server" Text="Mis reclamos" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnReclamos_Click"/>
        </section>
    </div>

</asp:Content>