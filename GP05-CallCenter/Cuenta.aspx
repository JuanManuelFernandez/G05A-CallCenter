<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="CallCenter.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 class="text-center">Mi cuenta</h1>

        <div class="text-center mt-3">
            <asp:LinkButton ID="BtnMod" runat="server" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnMod_Click" Style="display: inline-flex; align-items: center;">
                <img src="Assets/edit.png" style="width: 30px; height: 30px; margin-right: 5px;" />Cambiar contraseña </asp:LinkButton>
        </div>
        <div class="text-center mt-3">
            <asp:LinkButton ID="BntDel" runat="server" CssClass="btn btn-danger btn-lg mx-3" OnClick="BntDel_Click" Style="display: inline-flex; align-items: center;">
                <img src="Assets/cancel.png" style="width: 30px; height: 30px; margin-right: 5px;" />Eliminar cuenta </asp:LinkButton>
        </div>
        <div class="text-center mt-3">
            <asp:Label ID="lblConfirmar" runat="server" Text="" Visible="false"></asp:Label>
            <asp:TextBox ID="txtPass" runat="server" Visible="false" TextMode="Password" required="required"></asp:TextBox>
        </div>
        <div class="text-center mt-3">
            <asp:Label ID="lblReconfirmar" runat="server" Text="" Visible="false"></asp:Label>
            <asp:TextBox ID="txtRePass" runat="server" Visible="false" TextMode="Password" required="required"></asp:TextBox>
        </div>
        <div class="text-center mt-3">
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" Visible="false" CssClass="btn-danger" OnClick="btnConfirmar_Click" />
            <asp:Button ID="btnConfirmarCambio" runat="server" Text="Confirmar" Visible="false" CssClass="btn-danger" OnClick="btnConfirmarCambio_Click" />
            <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    </main>
</asp:Content>
