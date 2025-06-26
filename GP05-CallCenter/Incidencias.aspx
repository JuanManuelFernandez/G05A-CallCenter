<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencias.aspx.cs" Inherits="CallCenter.Incidencias" %>

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
                    <asp:TextBox ID="txtIdCliente" runat="server" placeholder="DNI"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblMail" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de Incidencia"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
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
                    <asp:DropDownList ID="ddlPrioridad" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblResolucion" runat="server" Text="Resolucion"></asp:Label>
                    <asp:TextBox ID="txtResolucion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 250px; resize: none;"></asp:TextBox>
                </div>
                <div class="text-start">
                    <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="btn btn-primary btn-lg mx-3" onclick="btnCargar_Click"/>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary btn-lg mx-3" />
                </div>
            </div>
        </div>

    </main>
</asp:Content>
