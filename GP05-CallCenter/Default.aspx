<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallCenter._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle" class="text-center py-3 px-4 mb-4 bg-primary text-white display-1">Call Center UTN</h1>
            <div class="text-center">
                <img src="assets/LogoUTN.png" alt="Alternate Text" class="img-fluid mt-2 w-25 h-100" />
            </div>
            <%--aclaracion: mb = margin bot (margin de abajo) mt = margin top (margin de arriba)--%>
            <p class="text-center mt-2 mb-4">
                <br>
                Bienvenido a nuestro centro de soporte.
                <br>
                Tomaremos tu reclamo con mucho gusto, te esperamos.
            </p>
        </section>

        <div>
            <h2 class="text-center mb-4">Elige una opcion:</h2>
        </div>
        <div class="row justify-content-center mt-4">
            <div class="col-md-3 text-center">
                <div class="d-grid gap-3">
                    <asp:Button ID="btnLogin" runat="server" Text="Inicia sesión" CssClass="btn btn-primary btn-lg" OnClick="btnLogIn_Click" />

                    <asp:Button ID="btnRegister" runat="server" Text="Registrate" CssClass="btn btn-primary btn-lg" OnClick="btnRegister_Click" />
                </div>
            </div>
        </div>

    </main>

</asp:Content>
