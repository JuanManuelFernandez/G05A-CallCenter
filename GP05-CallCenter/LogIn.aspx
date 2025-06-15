<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="CallCenter.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 class="text-center">¡Bienvenido de vuelta!</h1>

        <p class="text-center mb-0">¡Que bueno volver a verte!</p>
        <p class="text-center">Tomaremos tu reclamo lo más pronto posible. Ya sabes que hacer en los campos de abajo.</p>

        <div class="text-center">
            <div class="mb-3">
                <asp:TextBox ID="txtMail" runat="server" Placeholder="Mail"></asp:TextBox>
            </div>
            <div class="mt-3">
                <asp:TextBox ID="txtContraseña" runat="server" Placeholder="Contraseña"></asp:TextBox>
            </div>
            <div class="row justify-content-center mt-3">
                <section class="col-md-3" aria-labelledby="hostingTitle">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnIngresar_Click" />
                </section>
            </div>
        </div>
    </main>
</asp:Content>
