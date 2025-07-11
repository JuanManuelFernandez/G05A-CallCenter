﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencia.aspx.cs" Inherits="GP05_CallCenter.Incidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblFechaYHora" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="col-md-1">
                    <asp:Label ID="lblIdCliente" runat="server" Text="DNI"></asp:Label>
                    <asp:TextBox ID="txtIdCliente" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblMail" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblDireccion" runat="server" Text="Direccion"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de Incidencia"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
                    <%-- Cambiar a DropDownList o derivados... --%>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 250px; resize: none;"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 text-end">
                <div class="col-md-1">
                    <asp:Label ID="lblEstadoActual" runat="server" Text="Estado Actual"></asp:Label>
                    <asp:TextBox ID="txtEstadoActual" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad"></asp:Label>
                    <asp:TextBox ID="txtPrioridad" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblResolucion" runat="server" Text="Resolucion"></asp:Label>
                    <asp:TextBox ID="txtResolucion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 250px; resize: none;"></asp:TextBox>
                </div>
                <div class="text-start">
                    <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="btn btn-primary btn-lg mx-3" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary btn-lg mx-3" />
                </div>
            </div>
        </div>

    </main>
</asp:Content>
