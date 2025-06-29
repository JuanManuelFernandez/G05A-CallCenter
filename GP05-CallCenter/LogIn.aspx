<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="CallCenter.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="text-center mb-4">
            <h1 class="py-3 mb-4 bg-primary text-white display-4" style="display: inline-block;">¡Bienvenido de vuelta!</h1>
            <p>
                Tomaremos tu reclamo lo más pronto posible.
            <br>
                Por favor, completa con tus datos debajo.
            </p>
        </div>

        <div class="d-flex align-items-center justify-content-center">
            <div class="col-md-2">
                <div class="form-group mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group mb-3">
                    <asp:TextBox ID="txtClave" runat="server" Placeholder="********" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnIngresar_Click" />
        </section>
    </div>
    <div class="text-center mt-1">
        <asp:Label ID="lblError" runat="server" Text="Email o Clave incorrecto..." Visible="false" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
