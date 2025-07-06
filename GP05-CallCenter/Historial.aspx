<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="GP05_CallCenter.Historial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1>Historial</h1>

        <asp:GridView ID="dgvHistorial" runat="server" class="table table-bordered" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IDIncidencia" HeaderText="ID" />
                <asp:BoundField DataField="EstadoActual" HeaderText="Estado actual" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
