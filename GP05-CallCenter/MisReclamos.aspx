<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisReclamos.aspx.cs" Inherits="GP05_CallCenter.MisReclamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h1>Tus reclamos historicos son los siguientes:</h1>
    </div>
    <asp:GridView ID="gdvReclamos" runat="server" AutoGenerateColumns="false" class="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="Tipo de Incidencia" DataField="IDTipo" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Estado Actual" DataField="EstadoActual" />
            <asp:BoundField HeaderText="Fecha Creacion" DataField="FechaYHoraCreacion" />
            <asp:BoundField HeaderText="Detalle de Resolucion" DataField="Resolucion" />
            <asp:BoundField HeaderText="Fecha Resolucion" DataField="FechaYHoraResolucion" />
        </Columns>
    </asp:GridView>
</asp:Content>
