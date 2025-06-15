<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="CallCenter.Cuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 class="text-center">Mi cuenta</h1>

        <div class="text-center">
            <asp:Button ID="BtnMod" runat="server" Text="Cambiar contraseña" CssClass="btn btn-primary btn-lg mx-3" />
        </div>
        <div class="text-center mt-3">
            <asp:Button ID="BntDel" runat="server" Text="Eliimnar cuenta" CssClass="btn btn-danger btn-lg mx-3" />
        </div>
    </main>
</asp:Content>
