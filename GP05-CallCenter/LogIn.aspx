<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="CallCenter.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="text-center">
            <h1 class="py-3 mb-4 bg-primary text-white display-4" style="display: inline-block;">¡Bienvenido de vuelta!</h1>
        </div>

        <p class="text-center">Tomaremos tu reclamo lo más pronto posible. <br> Por favor, completa con tus datos debajo.</p>

        <div class="text-center">
            <div class="mb-3">
                <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
            </div>
            <div class="mt-3">
                <asp:TextBox ID="txtClave" runat="server" Placeholder="********" TextMode="Password"></asp:TextBox>
            </div>
            <div class="row justify-content-center mt-3">
                <section class="col-md-3" aria-labelledby="hostingTitle">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnIngresar_Click"/>
                </section>
            </div>
             <div class="row justify-content-center mt-1">
                <asp:Label ID="lblError" runat="server" Text="Email o Clave incorrecto..." Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </main>
</asp:Content>
