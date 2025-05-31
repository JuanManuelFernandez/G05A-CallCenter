<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="GP05_CallCenter.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 class="text-center">¡Bienvenido de vuelta!</h1>

        <p class="text-center mb-0">¡Que bueno volver a verte!</p>
        <p class="text-center">Tomaremos tu reclamo lo más pronto posible. Ya sabes que hacer en los campos de abajo.</p>

        <div class="text-center">
            <div class="mb-3">
                <asp:TextBox ID="TextBox1" runat="server" Placeholder="Mail"></asp:TextBox>
            </div>
            <div class="mt-3">
                <asp:TextBox ID="TextBox2" runat="server" Placeholder="Contraseña"></asp:TextBox>
            </div>
        </div>
    </main>
</asp:Content>
