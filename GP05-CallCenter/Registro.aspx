<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GP05_CallCenter.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 class="text-center">¡Bienvenido!</h1>

        <p class="text-center">¿Tu primera vez acá? registrate y tomaremos tu pedido lo más pronto posible</p>

        <div class="text-center">
            <div class="mb-3">
                <asp:TextBox ID="TextBox1" runat="server" Placeholder="DNI"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="TextBox2" runat="server" Placeholder="Nombre"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="TextBox3" runat="server" Placeholder="Apellido"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:TextBox ID="TextBox4" runat="server" Placeholder="Mail"></asp:TextBox>
            </div>
            <div class="mt-3">
                <asp:TextBox ID="TextBox5" runat="server" Placeholder="Telefono"></asp:TextBox>
            </div>
        </div>
    </main>
</asp:Content>
