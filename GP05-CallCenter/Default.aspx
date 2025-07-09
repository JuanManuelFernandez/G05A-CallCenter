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
                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" OnClick="BtnLogIn_Click">
                    <img src="Assets/Login.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                    Inicia sesión
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-primary btn-lg d-flex justify-content-center align-items-center" OnClick="BtnRegister_Click">
                    <img src="Assets/Registrar.png" style="width: 30px; height: 30px; margin-right: 10px;" />
                    Registrate
                    </asp:LinkButton>

                </div>
            </div>
        </div>

    </main>

</asp:Content>
