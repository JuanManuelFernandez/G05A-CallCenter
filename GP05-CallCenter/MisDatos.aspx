﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="misdatos.aspx.cs" Inherits="CallCenter.Misdatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="text-center mb-4">
            <h1>Tus datos son los siguientes:</h1>
        </div>

        <div class="d-flex align-items-center justify-content-center">
            <div class="col-md-2">
                <div class="form-group mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group mb-3">
                    <asp:Label ID="lblDNI" runat="server" Text="DNI" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group mb-3">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group mb-3">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group mb-3">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" min="1"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:LinkButton ID="btnModificar" runat="server" CssClass="btn btn-primary btn-lg mx-3" OnClick="btnModificar_Click" Style="display: inline-flex; align-items: center;">
                <img src="Assets/edit.png" style="width: 30px; height: 30px; margin-right: 5px;" />Modificar </asp:LinkButton>

            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-lg mx-3 mt-3" OnClick="btnCancelar_Click" Style="display: inline-flex; align-items: center;">
                <img src="Assets/noLike.png" style="width: 30px; height: 30px; margin-right: 5px;" />Cancelar </asp:LinkButton>
        </section>
    </div>
    <div class="row text-center mt-1">
        <asp:Label ID="lblRegistro" runat="server" Text="Ya existe el dato ingresado..." Visible="false" ForeColor="Red" />
    </div>
</asp:Content>
