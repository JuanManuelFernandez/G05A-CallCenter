<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formularios.aspx.cs" Inherits="CallCenter.Formularios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1>Lista de Incidencias Actuales:</h1>
        <asp:GridView ID="dgvIncidencias" OnSelectedIndexChanged="dgvIncidencias_SelectedIndexChanged" DataKeyNames="IdIncidencia" runat="server" class="table table-bordered" AutoGenerateColumns="false" OnRowCommand="dgvIncidencias_RowCommand" OnRowDataBound="dgvIncidencias_RowDataBound">
            <Columns>
                <%--<asp:BoundField HeaderText="ID Cliente" DataField="IdCliente" />--%>
                <asp:BoundField HeaderText="Prioridad" DataField="prioridad.Nombre" />
                <asp:BoundField HeaderText="Estado Actual" DataField="EstadoActual" />
                <asp:BoundField HeaderText="Fecha/Hora Creacion" DataField="FechaYHoraCreacion" />
                <asp:TemplateField HeaderText="Historial">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="btnHistorial" runat="server" CommandName="Abrir" CommandArgument='<%# Container.DataItemIndex %>' Text="Abrir" CssClass="btn btn-primary btn-lg"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asignar Empleado" Visible="false">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="form-control form-control-rounded" OnSelectedIndexChanged="ddlEmpleados_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>

</asp:Content>
