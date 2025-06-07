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
            <div class="mb-3">
                <asp:TextBox ID="TextBox5" runat="server" Placeholder="Telefono"></asp:TextBox>
            </div>
            <div class="mt-3">
                 <asp:TextBox ID="TextBox6" runat="server" Placeholder="Contraseña"></asp:TextBox>
            </div>
            <div class="row justify-content-center mt-1">
                <asp:Label ID="LblRegistro" runat="server" Text="Registro realizado con exito" Visible="false" ForeColor="Green"></asp:Label>
            </div>
            <div class="row justify-content-center mt-2">
                <section class="col-md-3" aria-labelledby="hostingTitle">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnRegistrar_Click" />
                </section>
            </div>
        </div>
    </main>
</asp:Content>
