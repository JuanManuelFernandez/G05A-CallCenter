<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formularios.aspx.cs" Inherits="GP05_CallCenter.Formularios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1>Lista de Incidencias Actuales:</h1>
        <asp:GridView ID="dgvIncidencias" OnSelectedIndexChanged="dgvIncidencias_SelectedIndexChanged" DataKeyNames="IdIncidencia" runat="server" class="table table-bordered" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Fecha y Hora" DataField="FechaYHoraCreacion" />
                <asp:BoundField HeaderText="Prioridad" DataField="IdPrioridad" />
                <asp:CommandField ShowSelectButton="true" SelectText="Abrir" />
            </Columns>
        </asp:GridView>
        
    </main>

</asp:Content>
