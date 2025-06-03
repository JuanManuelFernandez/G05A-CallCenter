<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencia.aspx.cs" Inherits="GP05_CallCenter.Incidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblFechaYHora" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblDNI" runat="server" Text="DNI"></asp:Label>
                <asp:TextBox ID="txtDNI" runat="server" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 250px; resize: none;" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </main>
</asp:Content>
